namespace Beacons.Models
{
    public class Coords
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }

        public override string ToString()
        {
            return $"{Latitude:N3}, {Longitude:N3}, accurate to {Accuracy:N2}m";
        }
    }
}
