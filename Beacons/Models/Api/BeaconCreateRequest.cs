namespace Beacons.Models.Api
{
    public class BeaconCreateRequest
    {
        public BeaconModel Beacon { get; }

        public BeaconCreateRequest(BeaconModel beacon)
        {
            Beacon = beacon;
        }
    }
}