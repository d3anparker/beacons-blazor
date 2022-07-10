using Beacons.Services.Beacons;
using Beacons.Services.BeaconSharing;
using Beacons.Services.Distances;
using Beacons.Services.Location;

namespace Beacons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeaconServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBeaconService, TestBeaconService>()
                .AddTransient<IBeaconSharingService, BeaconSharingService>()
                .AddSingleton<IDistanceCalculator, DistanceCalculator>()
                .AddTransient<Watcher>()
                .AddSingleton(provider =>
                {
                    return new LocationWatcherFactory(provider.GetRequiredService<Watcher>);
                });
                
            return services;
        }
    }
}
