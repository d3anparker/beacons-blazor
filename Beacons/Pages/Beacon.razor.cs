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

        public Beacon()
        {
            GeoLocationAvailable = true;
        }

        protected override async Task OnInitializedAsync()
        {
            var @ref = DotNetObjectReference.Create(this);
            await Js.InvokeVoidAsync("startWatch", @ref);

            await base.OnInitializedAsync();
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
