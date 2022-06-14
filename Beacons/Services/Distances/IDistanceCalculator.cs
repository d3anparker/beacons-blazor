using Beacons.Models;

namespace Beacons.Services.Distances
{
    public interface IDistanceCalculator
    {
        DistanceResponse CalculateDistance(CalculateDistanceRequest request);
    }
}
