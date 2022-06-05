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
        }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
