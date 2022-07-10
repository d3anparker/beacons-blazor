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
        private ISubject<Position>? _positionSubject;
        private ISubject<bool>? _geoLocationAvailableSubject;

        public IObservable<Position>? NextPosition { get; private set; }
        public IObservable<bool>? GeoLocationAvailable { get; private set; }

        public Watcher(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
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

            _positionSubject = new Subject<Position>();
            NextPosition = _positionSubject.AsObservable();

            _geoLocationAvailableSubject = new Subject<bool>();
            GeoLocationAvailable = _geoLocationAvailableSubject.AsObservable();

            _currentWatchId = await _module.InvokeAsync<int>("startWatch");
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Watcher not initialised");
            }

            _positionSubject?.OnCompleted();
            await _module.InvokeVoidAsync("stopWatch", _currentWatchId);
        }

        [JSInvokable]
        public void SetLatestPosition(Position position)
        {
            if (_positionSubject is null)
            {
                throw new InvalidOperationException("Subject not created");
            }

            _positionSubject.OnNext(position);
        }

        [JSInvokable]
        public void SetGeoLocationNotAvailable()
        {
            _geoLocationAvailableSubject?.OnNext(false);
        }

        private async Task<IJSObjectReference> LoadModuleAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
            await module.InvokeVoidAsync("initialise", DotNetObjectReference.Create(this));

            return module;
        }
    }
}