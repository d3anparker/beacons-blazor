using Beacons.Models;
using Beacons.Services.Beacons;
using Beacons.Services.Distances;
using Beacons.Services.Location;
using Beacons.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Reactive.Disposables;

namespace Beacons.Pages
{
    public partial class Beacon : ComponentBase, IDisposable
    {
        [Inject] private IBeaconService BeaconService { get; set; }
        [Inject] private IDistanceCalculator DistanceCalculator { get; set; }
        [Inject] private LocationWatcherFactory LocationWatcherFactory { get; set; }

        [Parameter] public Guid BeaconId { get; set; }

        private Models.Beacon? _beacon;
        private readonly BeaconViewModel _model;

        private Watcher? _watcher;
        private CompositeDisposable _subscriptions;

        public Beacon()
        {
            _model = new BeaconViewModel()
            {
                GeoLocationAvailable = true
            };
            _subscriptions = new CompositeDisposable();
        }

        protected override async Task OnInitializedAsync()
        {
            _beacon = await BeaconService.GetByIdAsync(BeaconId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _watcher = await LocationWatcherFactory.CreateWatcherAsync();
                await StartWatchAsync();
            }
        }

        public async Task StartWatchAsync()
        {
            if(_watcher is null)
            {
                throw new InvalidOperationException("Watcher not created");
            }

            await _watcher.StartWatchAsync();

            _subscriptions.Clear();

            if (_watcher.NextPosition is not null)
            {
                var subscription = _watcher.NextPosition.Subscribe(SetLatestPosition);
                _subscriptions.Add(subscription);

                _model.Watching = true;
            }

            if(_watcher.GeoLocationAvailable is not null)
            {
                var subscription = _watcher.GeoLocationAvailable.Subscribe(x =>
                {
                    _model.GeoLocationAvailable = x;

                    StateHasChanged();
                });

                _subscriptions.Add(subscription);
            }

            StateHasChanged();
        }

        public async Task StopWatchAsync()
        {
            if (_watcher is null)
            {
                throw new InvalidOperationException("Watcher not created");
            }

            await _watcher.StopWatchAsync();

            _subscriptions.Clear();
            _model.Watching = false;

            StateHasChanged();
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

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void SetLatestPosition(Position position)
        {
            _model.CurrentPosition = position;

            if (_beacon != null)
            {
                var request = new CalculateDistanceRequest()
                {
                    CurrentCoords = position.Coords,
                    DestinationCoords = _beacon.Coords,
                };

                var response = DistanceCalculator.CalculateDistance(request);
                _model.CurrentDistance = response;
            }

            StateHasChanged();
        }
    }
}
