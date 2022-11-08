using Beacons.Models;
using Beacons.Models.Api;
using Beacons.Services.Beacons;
using Beacons.Services.BeaconSharing;
using Beacons.Services.Location;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reactive.Disposables;

namespace Beacons.Pages
{
    public partial class Create : ComponentBase, IDisposable
    {
        [Inject] private BeaconSharerFactory BeaconSharerFactory { get; set; } = default!;
        [Inject] private IBeaconService BeaconService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        [Inject] private LocationWatcherFactory LocationWatcherFactory { get; set; } = default!;

        private readonly CreateViewModel _model;

        private readonly CompositeDisposable _subscriptions;

        public Create()
        {
            _model = new CreateViewModel()
            {
                GeolocationAvailable = true
            };
            _subscriptions = new CompositeDisposable();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var watcher = await LocationWatcherFactory.CreateWatcherAsync();

                await watcher.StartWatchAsync();

                _subscriptions.Add(watcher.NextPosition.Subscribe(SetCurrentPosition, SetPositionError));
                _subscriptions.Add(watcher.GeoLocationAvailable.Subscribe(SetGeoLocationAvailable));
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        public async Task CreateBeacon(MouseEventArgs e)
        {
            if (_model.Position is null)
            {
                return;
            }

            _model.CreationError = null;
            SetCreating(true);

            var model = new BeaconModel()
            {
                Latitude = _model.Position.Coords.Latitude,
                Longitude = _model.Position.Coords.Longitude
            };

            var response = await BeaconService.CreateAsync(new BeaconCreateRequest(model));

            SetCreating(false);

            if (response?.Beacon is not null)
            {
                _model.ShowMap = true;
                await ShareBeaconAsync(response.Beacon.Id);
            }
            else
            {
                _model.CreationError = "Couldn't create beacon";
                StateHasChanged();
            }
        }

        private void SetCreating(bool creating)
        {
            _model.Creating = creating;
            StateHasChanged();
        }

        private async Task ShareBeaconAsync(Guid id)
        {
            var sharer = await BeaconSharerFactory.CreateBeaconSharerAsync();

            var request = new ShareDataRequest
            {
                Title = "Come and find me!",
                Text = "I'll be waiting here!",
                Url = NavigationManager.ToAbsoluteUri($"/beacon/{id}").ToString()
            };

            await sharer.ShareBeaconAsync(request);
        }

        private void SetCurrentPosition(Position position)
        {
            _model.Position = position;
            StateHasChanged();
        }

        private void SetGeoLocationAvailable(bool available)
        {
            _model.GeolocationAvailable = available;
            StateHasChanged();
        }

        private void SetPositionError(Exception exc)
        {
            _model.PositionError = exc.Message;
            StateHasChanged();
        }
    }
}
