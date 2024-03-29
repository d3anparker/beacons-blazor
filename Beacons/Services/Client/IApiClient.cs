﻿using Beacons.Models.Api;

namespace Beacons.Services.Client
{
    public interface IApiClient
    {
        Task<ApiResponse<BeaconCreationResponse>> CreateAsync(BeaconCreateRequest request);
        Task<ApiResponse<BeaconModel>> GetAsync(Guid beaconId);
    }
}
