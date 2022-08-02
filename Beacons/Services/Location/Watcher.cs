using Beacons.Models;
using Microsoft.JSInterop;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Beacons.Services.Location
{
    public class Watcher
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _module;
        private int? _currentWatchId;
        private readonly ISubject<Position> _positionSubject;
        private readonly ISubject<bool> _geoLocationAvailableSubject;

        public IObservable<Position> NextPosition { get; }
        public IObservable<bool> GeoLocationAvailable { get; }

        /// <summary>
        /// True if this Watcher has finished watching for changes and should be disposed.
        /// </summary>
        public bool Completed { get; private set; }

        public Watcher(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;

            _positionSubject = new Subject<Position>();
            NextPosition = _positionSubject.AsObservable();

            _geoLocationAvailableSubject = new Subject<bool>();
            GeoLocationAvailable = _geoLocationAvailableSubject.AsObservable();
        }

        public async Task InitialiseAsync()
        {
            _module = await LoadModuleAsync();
        }

        public async Task StartWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Watcher not initialised");
            }

            if (Completed)
            {
                throw new InvalidOperationException("Watcher already completed");
            }

            _currentWatchId = await _module.InvokeAsync<int>("startWatch");
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Watcher not initialised");
            }

            if (Completed)
            {
                throw new InvalidOperationException("Watcher already completed");
            }

            _positionSubject.OnCompleted();
            await _module.InvokeVoidAsync("stopWatch", _currentWatchId);

            Completed = true;
        }

        [JSInvokable]
        public void SetLatestPosition(Position position)
        {
            _positionSubject.OnNext(position);
        }

        [JSInvokable]
        public void SetGeoLocationNotAvailable()
        {
            _geoLocationAvailableSubject.OnNext(false);
            _geoLocationAvailableSubject.OnCompleted();

            Completed = true;
        }

        [JSInvokable]
        public void SetGeoLocationError(LocationError error)
        {
            _positionSubject.OnError(new Exception(error.Message));
            _positionSubject.OnCompleted();

            Completed = true;
        }

        private async Task<IJSObjectReference> LoadModuleAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
            await module.InvokeVoidAsync("initialise", DotNetObjectReference.Create(this));

            return module;
        }
    }

    public class LocationError
    {
        public string Message { get; set; } = string.Empty;
    }
}