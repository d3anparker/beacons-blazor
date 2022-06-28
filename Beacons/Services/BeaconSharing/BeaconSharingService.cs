using Beacons.Models;
using Microsoft.JSInterop;

namespace Beacons.Services.BeaconSharing
{
    public class BeaconSharingService : IBeaconSharingService
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _shareModule;

        public BeaconSharingService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<ShareDataResponse> ShareBeaconAsync(ShareDataRequest request)
        {
            while (_shareModule == null)
            {
                var loaded = await LoadModuleAsync();

                if(!loaded)
                {
                    return new ShareDataResponse();
                }
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

        private async Task<bool> LoadModuleAsync()
        {
            _shareModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Share.js");

            return _shareModule != null;
        }
    }
}
