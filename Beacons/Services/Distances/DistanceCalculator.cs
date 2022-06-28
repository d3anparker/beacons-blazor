using Beacons.Extensions;
using Beacons.Models;

namespace Beacons.Services.Distances
{
    public class DistanceCalculator : IDistanceCalculator
    {
        /// <summary>
        /// Taken from https://www.geeksforgeeks.org/program-distance-two-points-earth/#:~:text=For%20this%20divide%20the%20values,is%20the%20radius%20of%20Earth.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DistanceResponse CalculateDistance(CalculateDistanceRequest request)
        {
            var lon1 = request.DestinationCoords.Longitude.ToRadians();
            var lat1 = request.DestinationCoords.Latitude.ToRadians();
            var lon2 = request.CurrentCoords.Longitude.ToRadians();
            var lat2 = request.CurrentCoords.Latitude.ToRadians();

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            return new DistanceResponse(c)
            {
                Accuracy = request.CurrentCoords.Accuracy
            };
        }
    }
}
