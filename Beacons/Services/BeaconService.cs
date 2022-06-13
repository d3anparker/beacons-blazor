using Beacons.Models;

namespace Beacons.Services
{
    public class BeaconService : IBeaconService
    {
        public BeaconService()
        {
        }

        public Task<Beacon?> GetByIdAsync(Guid id)
        {
            return Task.FromResult<Beacon?>(null);
        }
    }
}