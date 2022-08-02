using Beacons.Models;
using Beacons.Services.Beacons;
using Beacons.Services.BeaconSharing;
using Beacons.Services.Location;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Beacons.Pages
{
    public partial class Create : ComponentBase
    {
        [Inject] private IBeaconSharingService BeaconSharingService { get; set; }
        [Inject] private IBeaconService BeaconService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private LocationWatcherFactory LocationWatcherFactory { get; set; }

        private readonly CreateViewModel _model;

        public Create()
        {
            _model = new CreateViewModel()
            {
                GeolocationAvailable = true
            };
        }

        protected override async Task OnInitializedAsync()
        {
            var watcher = await LocationWatcherFactory.CreateWatcherAsync();

            await watcher.StartWatchAsync();

            watcher.NextPosition.Subscribe(SetCurrentPosition, SetPositionError);
            watcher.GeoLocationAvailable.Subscribe(SetGeoLocationAvailable);

            await base.OnInitializedAsync();
        }

        public async Task Share(MouseEventArgs e)
        {
            var request = new ShareDataRequest
            {
                Title = "Come and find me!",
                Text = "I'll be waiting here!",
                Url = NavigationManager.ToAbsoluteUri($"/beacon/{Guid.NewGuid()}").ToString()
            };

            await BeaconSharingService.ShareBeaconAsync(request);
        }

        public async Task CreateBeacon(MouseEventArgs e)
        {
            await BeaconService.CreateAsync(new Services.Client.BeaconCreateRequest());
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
            _model.Error = exc.Message;
            StateHasChanged();
        }
    }
}
