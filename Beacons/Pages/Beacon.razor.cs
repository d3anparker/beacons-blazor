using Beacons.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Beacons.Pages
{
    public partial class Beacon : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        public bool GeoLocationAvailable { get; set; }
        public Position? Position { get; set; }
        public bool Watching { get; set; }
        private int _watchId;

        private readonly DotNetObjectReference<Beacon> _reference;

        public Beacon()
        {
            _reference = DotNetObjectReference.Create(this);
            GeoLocationAvailable = true;
        }

        protected override async Task OnInitializedAsync()
        {
            await StartWatchAsync();
            await base.OnInitializedAsync();
        }

        public async Task StartWatchAsync()
        {
            _watchId = await Js.InvokeAsync<int>("startWatch", _reference);
            Watching = true;
            StateHasChanged();
        }

        public async Task StopWatchAsync()
        {
            await Js.InvokeVoidAsync("stopWatch", _reference, _watchId);
            Watching = false;
            StateHasChanged();
        }

        [JSInvokable]
        public void SetGeoLocationNotAvailable()
        {
            GeoLocationAvailable = false;
        }

        [JSInvokable]
        public void SetLatestPosition(Position position)
        {
            Position = position;
            StateHasChanged();
        }

        
    }
}
