namespace Beacons.Models
{
    public class CreateViewModel
    {
        public bool GeolocationAvailable { get; set; }
        public string? PositionError { get; set; }
        public string? CreationError { get; set; }
        public Position? Position { get; set; }
        public bool Creating { get; set; }
        public bool ShowMap { get; set; }
    }
}
