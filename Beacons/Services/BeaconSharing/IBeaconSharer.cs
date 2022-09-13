using Beacons.Models;

namespace Beacons.Services.BeaconSharing
{
    public interface IBeaconSharer
    {
        Task<ShareDataResponse> ShareBeaconAsync(ShareDataRequest request);
    }
}
