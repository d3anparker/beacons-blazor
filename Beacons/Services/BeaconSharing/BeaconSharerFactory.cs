namespace Beacons.Services.BeaconSharing
{
    public class BeaconSharerFactory
    {
        private readonly Func<IInitialisableBeaconSharer> _createBeaconSharer;

        public BeaconSharerFactory(Func<IInitialisableBeaconSharer> createBeaconSharer)
        {
            _createBeaconSharer = createBeaconSharer;
        }

        public async Task<IBeaconSharer> CreateBeaconSharerAsync()
        {
            var service = _createBeaconSharer();
            await service.InitialiseAsync();

            return service;
        }
    }
}
