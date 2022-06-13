using Beacons.Services;

namespace Beacons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeaconService(this IServiceCollection services)
        {
            services.AddTransient<IBeaconService, TestBeaconService>();

            return services;
        }
    }
}
