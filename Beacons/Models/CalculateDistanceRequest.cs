namespace Beacons.Models
{
    public class CalculateDistanceRequest
    {
        public Coords CurrentCoords { get; set; }
        public Coords DestinationCoords { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
    }
}