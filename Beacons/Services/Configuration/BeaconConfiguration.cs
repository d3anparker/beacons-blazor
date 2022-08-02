namespace Beacons.Services.Configuration
{
    public class BeaconConfiguration : IBeaconConfiguration
    {
        private readonly IConfiguration _configuration;

        public BeaconConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiUrl => _configuration["ApiUrl"];
    }
}
