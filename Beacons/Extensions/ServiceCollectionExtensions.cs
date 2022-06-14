using Beacons.Services.Beacons;
using Beacons.Services.Distances;

namespace Beacons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeaconServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBeaconService, TestBeaconService>()
                .AddSingleton<IDistanceCalculator, DistanceCalculator>();


            return services;
        }
    }
}
