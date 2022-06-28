using Beacons.Models;

namespace Beacons.Services.BeaconSharing
{
    public interface IBeaconSharingService
    {
        Task<ShareDataResponse> ShareBeaconAsync(ShareDataRequest request);
    }
}
