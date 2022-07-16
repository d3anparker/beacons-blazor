using Beacons.Models;
using Beacons.Models.Api;
using Beacons.Services.Client;

namespace Beacons.Services.Beacons
{
    public class BeaconService : IBeaconService
    {
        private readonly IApiClient _apiClient;

        public BeaconService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<Beacon?> GetByIdAsync(Guid id)
        {
            return Task.FromResult<Beacon?>(null);
        }

        public async Task<BeaconCreationResponse> CreateAsync(BeaconCreateRequest request)
        {
            var response = await _apiClient.CreateAsync(request);

            if(response.Successful)
            {
                return response.Data!;
            }


            // TODO. Something sensible here!
            return new BeaconCreationResponse(null, null);
        }
    }
}