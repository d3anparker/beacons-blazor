using Beacons.Models.Api;
using System.Net;
using System.Net.Http.Json;

namespace Beacons.Services.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient client, ILogger<ApiClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ApiResponse<BeaconCreationResponse>> CreateAsync(BeaconCreateRequest request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("/api/beacon", request);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    if (response.Headers.Location is not null)
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

                _logger.LogError($"CreateAsync failed: {response.StatusCode} - {response.ReasonPhrase}");
                return new ApiResponse<BeaconCreationResponse>(new[] { response.ReasonPhrase ?? $"Beacon not created - {response.StatusCode}" });

            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"CreateAsync failed: {e.StatusCode} - {e.Message}");
                return new ApiResponse<BeaconCreationResponse>(new[] { $"{e.StatusCode}", e.Message });
            }
        }

        public async Task<ApiResponse<BeaconModel>> GetAsync(Guid beaconId)
        {
            var url = $"/api/beacon/{beaconId}";
            try
            {
                var response = await _client.GetFromJsonAsync<BeaconModel>(url);

                return new ApiResponse<BeaconModel>()
                {
                    Data = response
                };
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"GetAsync failed for request: {url} with {e.StatusCode} - {e.Message}");
                return new ApiResponse<BeaconModel>(new[] { $"{e.StatusCode}", e.Message });
            }
        }
    }
}
