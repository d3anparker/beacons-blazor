using Beacons.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Beacons.Pages
{
    public partial class Beacon : ComponentBase, IAsyncDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        public bool GeoLocationAvailable { get; set; }
        public Position? Position { get; set; }
        public bool Watching { get; set; }
        private int _watchId;

        private readonly DotNetObjectReference<Beacon> _reference;
        private IJSObjectReference? _module;

        public Beacon()
        {
            _reference = DotNetObjectReference.Create(this);
            GeoLocationAvailable = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await LoadModuleAsync();
                await StartWatchAsync();
            }
        }

        public async Task StartWatchAsync()
        {
            if(_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            _watchId = await _module.InvokeAsync<int>("startWatch", _reference);
            Watching = true;
            StateHasChanged();
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            await _module.InvokeVoidAsync("stopWatch", _reference, _watchId);
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

        public async ValueTask DisposeAsync()
        {
            if(_module is not null)
            {
                await _module.DisposeAsync();
            }
        }

        private async Task LoadModuleAsync()
        {
            _module = await Js.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
        }
    }
}
