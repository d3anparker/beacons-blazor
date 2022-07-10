namespace Beacons.Services.Location
{
    public class LocationWatcherFactory
    {
        private readonly Func<Watcher> _createWatcher;

        public LocationWatcherFactory(Func<Watcher> createWatcher)
        {
            _createWatcher = createWatcher;
        }

        public async Task<Watcher> CreateWatcherAsync()
        {
            var watcher = _createWatcher();

            await watcher.InitialiseAsync();

            return watcher;
        }
    }
}