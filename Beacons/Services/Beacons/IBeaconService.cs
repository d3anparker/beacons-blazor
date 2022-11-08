using Beacons.Models.Api;

namespace Beacons.Services.Beacons
{
    public interface IBeaconService
    {
        Task<BeaconCreationResponse> CreateAsync(BeaconCreateRequest request);
        Task<BeaconModel?> GetByIdAsync(Guid id);
    }

    public class TestBeaconService : IBeaconService
    {
        private BeaconModel beacon = new BeaconModel()
        {
            Longitude = 0.1779022,
            Latitude = 51.8689552
        };

        public Task<BeaconCreationResponse> CreateAsync(BeaconCreateRequest request)
        {
            return Task.FromResult(new BeaconCreationResponse(null, null));
        }

        public Task<BeaconModel?> GetByIdAsync(Guid id) => Task.FromResult<BeaconModel?>(beacon);
    }
}