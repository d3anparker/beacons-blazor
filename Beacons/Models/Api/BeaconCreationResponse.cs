namespace Beacons.Models.Api
{
    public class BeaconCreationResponse
    {
        public BeaconModel Beacon { get; }
        public string Url { get; }

        public BeaconCreationResponse(BeaconModel beacon, string url)
        {
            Beacon = beacon;
            Url = url;
        }
    }
}