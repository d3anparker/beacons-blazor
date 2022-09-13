using Beacons.Models;
using Microsoft.JSInterop;

namespace Beacons.Services.BeaconSharing
{
    public class BeaconSharer : IBeaconSharer
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _shareModule;

        public BeaconSharer(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitialiseAsync()
        {
            _shareModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Share.js");
        }

        public async Task<ShareDataResponse> ShareBeaconAsync(ShareDataRequest request)
        {
            if(_shareModule == null)
            {
                throw new InvalidOperationException($"{nameof(BeaconSharer)} not initialised");
            }

            try
            {
                await _shareModule.InvokeVoidAsync("share", request);
            }
            catch (Exception e)
            {

            }

            return new ShareDataResponse();
        }
    }
}
