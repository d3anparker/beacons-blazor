using Microsoft.JSInterop;

namespace Beacons.Services.BeaconSharing
{
    public class BeaconSharerFactory
    {
        private readonly IJSRuntime _jSRuntime;

        public BeaconSharerFactory(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task<IBeaconSharer> CreateBeaconSharerAsync()
        {
            var service = new BeaconSharer(_jSRuntime);
            await service.InitialiseAsync();

            return service;
        }
    }
}
