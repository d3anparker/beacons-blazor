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
        private ISubject<Position>? _subject;

        public IObservable<Position>? NextPosition { get; private set; }

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

            _subject = new Subject<Position>();
            NextPosition = _subject.AsObservable();

            _currentWatchId = await _module.InvokeAsync<int>("startWatch");
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Watcher not initialised");
            }

            await _module.InvokeVoidAsync("stopWatch", _currentWatchId);
        }

        [JSInvokable]
        private void SetLatestPosition(Position position)
        {
            if (_subject is null)
            {
                throw new InvalidOperationException("Subject not created");
            }

            _subject.OnNext(position);
        }

        private async Task<IJSObjectReference> LoadModuleAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
            await module.InvokeVoidAsync("initialise", DotNetObjectReference.Create(this));

            return module;
        }
    }

}