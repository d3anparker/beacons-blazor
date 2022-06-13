using Beacons.Models;

namespace Beacons.Services
{
    public interface IBeaconService
    {
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

        public Task<Beacon?> GetByIdAsync(Guid id) => Task.FromResult<Beacon?>(beacon);
    }
}