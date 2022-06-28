namespace Beacons.Models
{
    public class DistanceResponse
    {
        public double MetricDistance { get; }
        public double ImperialDistance { get; }
        public double Accuracy { get; set; }

        public DistanceResponse(double distance)
        {
            MetricDistance = distance * 6371;
            ImperialDistance = distance * 3956;
        }
    }
}
