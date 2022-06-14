namespace Beacons.Models
{
    public class DistanceResponse
    {
        public double MetricDistance { get; }
        public double ImperialDistance { get; }

        public DistanceResponse(double distance)
        {
            MetricDistance = distance * 6371;
            ImperialDistance = distance * 3956;
        }
    }
}
