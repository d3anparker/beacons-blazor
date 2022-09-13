using Beacons.Services.Beacons;
using Beacons.Services.BeaconSharing;
using Beacons.Services.Client;
using Beacons.Services.Configuration;
using Beacons.Services.Distances;
using Beacons.Services.Location;

namespace Beacons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeaconServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBeaconService, BeaconService>()
                .AddTransient<IBeaconSharingService, BeaconSharingService>()
                .AddSingleton<IDistanceCalculator, DistanceCalculator>()
                .AddSingleton<IBeaconConfiguration, BeaconConfiguration>()
                .AddTransient<Watcher>()
                .AddSingleton(provider =>
                {
                    return new LocationWatcherFactory(provider.GetRequiredService<Watcher>);
                });

            services.AddHttpClient<IApiClient, ApiClient>((provider, client) =>
            {
                var config = provider.GetRequiredService<IBeaconConfiguration>();
                client.BaseAddress = new Uri(config.ApiUrl);
            });

            return services;
        }
    }
}
