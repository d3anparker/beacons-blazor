namespace Beacons.Services.BeaconSharing
{
    public interface IInitialisableBeaconSharer : IBeaconSharer
    {
        Task InitialiseAsync();
    }
}
