using Beacons.Models;
using Beacons.Services.Beacons;
using Beacons.Services.Distances;
using Beacons.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Beacons.Pages
{
    public partial class Beacon : ComponentBase, IAsyncDisposable
    {
        [Inject] private IJSRuntime Js { get; set; } = default!;
        [Inject] private IBeaconService BeaconService { get; set; }
        [Inject] private IDistanceCalculator DistanceCalculator { get; set; }

        [Parameter] public Guid BeaconId { get; set; }

        private Models.Beacon? beacon;
        private readonly BeaconViewModel _model;

        private IJSObjectReference? _module;

        public Beacon()
        {
            _model = new BeaconViewModel()
            {
                GeoLocationAvailable = true
            };
        }

        protected override async Task OnInitializedAsync()
        {
            beacon = await BeaconService.GetByIdAsync(BeaconId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await LoadModuleAsync();
                await StartWatchAsync();
            }
        }

        public async Task StartWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            _model.CurrentWatchId = await _module.InvokeAsync<int>("startWatch");
            _model.Watching = true;

            StateHasChanged();
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            await _module.InvokeVoidAsync("stopWatch", _model.CurrentWatchId);
            _model.Watching = false;

            StateHasChanged();
        }

        [JSInvokable]
        public void SetGeoLocationNotAvailable()
        {
            _model.GeoLocationAvailable = false;
            StateHasChanged();
        }

        [JSInvokable]
        public void SetLatestPosition(Position position)
        {
            _model.CurrentPosition = position;

            if (beacon != null)
            {
                var request = new CalculateDistanceRequest()
                {
                    CurrentCoords = position.Coords,
                    DestinationCoords = beacon.Coords,
                };

                var response = DistanceCalculator.CalculateDistance(request);
                _model.CurrentDistance = response;
            }

            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            if (_module is not null)
            {
                await _module.DisposeAsync();
            }
        }

        public void ChangeUnit(ChangeEventArgs e)
        {
            if (e.Value is null)
            {
                return;
            }

            _model.CurrentUnit = Enum.Parse<DistanceUnit>(e.Value.ToString());
            StateHasChanged();
        }

        private async Task<IJSObjectReference> LoadModuleAsync()
        {
            var module = await Js.InvokeAsync<IJSObjectReference>("import", "./js/main.js");
            await module.InvokeVoidAsync("initialise", DotNetObjectReference.Create(this));

            return module;
        }
    }
}
