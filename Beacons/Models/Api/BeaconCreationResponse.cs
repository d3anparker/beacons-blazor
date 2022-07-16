namespace Beacons.Models.Api
{
    public class BeaconCreationResponse
    {
        public Beacon Beacon { get; }
        public string Url { get; }

        public BeaconCreationResponse(Beacon beacon, string url)
        {
            Beacon = beacon;
            Url = url;
        }
    }
}