namespace Beacons.Models
{
    public class CreateViewModel
    {
        public bool GeolocationAvailable { get; set; }
        public string? Error { get; set; }
        public Position? Position { get; set; }
        public bool Creating { get; set; }
    }
}
