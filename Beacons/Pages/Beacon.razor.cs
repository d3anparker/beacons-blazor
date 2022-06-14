using Beacons.Models;
using Beacons.Services.Beacons;
using Beacons.Services.Distances;
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
        private bool geoLocationAvailable { get; set; }
        private Position? currentPosition { get; set; }
        private bool watching { get; set; }
        private int _watchId;
        private DistanceResponse _distance;
        private DistanceUnit _displayedIn;
        private string currentDistance => _displayedIn == DistanceUnit.Metric ? $"{_distance.MetricDistance} KM" : $"{_distance.ImperialDistance} miles";

        private IJSObjectReference? _module;

        public Beacon()
        {
            geoLocationAvailable = true;
        }

        protected override async Task OnInitializedAsync()
        {
            beacon = await BeaconService.GetByIdAsync(BeaconId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _module = await LoadModuleAsync();
                await StartWatchAsync();
            }
        }

        public async Task StartWatchAsync()
        {
            if(_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            _watchId = await _module.InvokeAsync<int>("startWatch");
            watching = true;
            StateHasChanged();
        }

        public async Task StopWatchAsync()
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module not loaded");
            }

            await _module.InvokeVoidAsync("stopWatch", _watchId);
            watching = false;
            StateHasChanged();
        }

        [JSInvokable]
        public void SetGeoLocationNotAvailable()
        {
            geoLocationAvailable = false;
        }

        [JSInvokable]
        public void SetLatestPosition(Position position)
        {
            currentPosition = position;

            var request = new CalculateDistanceRequest()
            {
                CurrentCoords = position.Coords,
                DestinationCoords = beacon.Coords,
            };

            var response = DistanceCalculator.CalculateDistance(request);
            _distance = response;

            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            if(_module is not null)
            {
                await _module.DisposeAsync();
            }
        }

        public void ChangeUnit(ChangeEventArgs e)
        {
            if(e.Value is null)
            {
                return;
            }

            _displayedIn = Enum.Parse<DistanceUnit>(e.Value.ToString());
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
