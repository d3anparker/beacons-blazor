using Beacons.Models.Api;
using System.Net.Http.Json;
using System.Net;

namespace Beacons.Services.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<BeaconCreationResponse>> CreateAsync(BeaconCreateRequest request)
        {
            var response = await _client.PostAsJsonAsync("/api/beacon", request);

            if(response.StatusCode == HttpStatusCode.Created)
            {
                if(response.Headers.Location is not null)
                {
                    var url = response.Headers.Location.ToString();
                    var beacon = await response.Content.ReadFromJsonAsync<BeaconModel>();

                    if (beacon is not null)
                    {
                        var creationResponse = new BeaconCreationResponse(beacon, url);

                        return new ApiResponse<BeaconCreationResponse>()
                        {
                            Data = creationResponse
                        };
                    }
                }
            }

            return new ApiResponse<BeaconCreationResponse>(new List<string> { response.ReasonPhrase ?? $"Beacon not created - {response.StatusCode}" });
        }
    }
}
