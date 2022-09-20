using Beacons.Models;
using Microsoft.JSInterop;

namespace Beacons.Services.BeaconSharing
{
    public class BeaconSharer : IInitialisableBeaconSharer
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _sharerInstance;

        public BeaconSharer(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitialiseAsync()
        {
            var shareModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
            _sharerInstance = await shareModule.InvokeAsync<IJSObjectReference>("createSharer");
        }

        public async Task<ShareDataResponse> ShareBeaconAsync(ShareDataRequest request)
        {
            if(_sharerInstance == null)
            {
                throw new InvalidOperationException($"{nameof(BeaconSharer)} not initialised");
            }

            try
            {
                await _sharerInstance.InvokeVoidAsync("share", request);
            }
            catch (Exception e)
            {

            }

            return new ShareDataResponse();
        }
    }
}
