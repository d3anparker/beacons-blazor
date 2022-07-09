//using Beacons.Models;
//using Microsoft.JSInterop;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;

//namespace Beacons.Services.Location
//{
//    public interface ILocationWatchService
//    {
//        Task<WatchState> StartWatchAsync(Action<Position> onNext);

//        Task StopWatchAsync(WatchState watchState);
//    }

//    public class LocationService : ILocationWatchService, IAsyncDisposable
//    {
//        private ISubject<Position> _subject;
//        private IDisposable _subscription;
//        private readonly IJSRuntime _jSRuntime;

//        private IJSObjectReference _module;

//        public LocationService(IJSRuntime jSRuntime)
//        {
//            _jSRuntime = jSRuntime;
//        }

//        public ValueTask DisposeAsync()
//        {
//            _subject?.OnCompleted();
//            _subscription?.Dispose();

//            return ValueTask.CompletedTask;
//        }

//        public async Task<WatchState> StartWatchAsync(Action<Position> onNext)
//        {
//            _module = _module ?? await LoadModuleAsync();

//            var subject = new Subject<Position>();
//            var subscription = subject.Subscribe(onNext);

//            var watchId = await _module.InvokeAsync<int>("startWatch");

//            var watchState = new WatchState(subject, subscription, watchId);

//            return watchState;
//        }

//        public async Task StopWatchAsync(WatchState watchState)
//        {
//            watchState.Subscription.Dispose();

//            await _module.InvokeVoidAsync("stopWatch", watchState.WatchId);

//            throw new NotImplementedException();
//        }

        
//    }

//    public record WatchState(Watcher Watcher, int WatchId);
    
//    public class WatchObserver
//    {
//        private readonly Subject<Position> _subject;

//        public WatchObserver()
//        {
//            _subject = new Subject<Position>();
//        }

//        public IObservable<Position> GetPosition() => _subject.AsObservable();

//        [JSInvokable]
//        public void SetLatestPosition(Position position)
//        {
//        }
//    }
//}
