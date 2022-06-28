using Beacons.Models;
using Humanizer;

namespace Beacons.ViewModels
{
    public class BeaconViewModel
    {
        public bool GeoLocationAvailable { get; set; }
        public Position? CurrentPosition { get; set; }
        public DistanceResponse? CurrentDistance { get; set; }
        public DistanceUnit CurrentUnit { get; set; } = DistanceUnit.Metric;
        public bool Watching { get; set; }
        public int CurrentWatchId { get; set; }

        public string CurrentDistanceDescription => CurrentUnit == DistanceUnit.Metric ?
            $"{CurrentDistance?.MetricDistance.ToMetric(MetricNumeralFormats.WithSpace, 2)} KM" :
            $"{CurrentDistance?.ImperialDistance} miles";
    }
}
