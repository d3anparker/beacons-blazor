using Beacons.Models;
using Beacons.Models.Api;
using Beacons.Services.Client;

namespace Beacons.Services.Beacons
{
    public interface IBeaconService
    {
        Task<BeaconCreationResponse> CreateAsync(BeaconCreateRequest request);
        Task<Beacon?> GetByIdAsync(Guid id);
    }

    public class TestBeaconService : IBeaconService
    {
        private Beacon beacon = new Beacon()
        {
            Coords = new Coords()
            {
                Latitude = 51.8689552,
                Longitude = 0.1779022
            }
        };

        public Task<BeaconCreationResponse> CreateAsync(BeaconCreateRequest request)
        {
            return Task.FromResult(new BeaconCreationResponse(null, null));
        }

        public Task<Beacon?> GetByIdAsync(Guid id) => Task.FromResult<Beacon?>(beacon);
    }
}