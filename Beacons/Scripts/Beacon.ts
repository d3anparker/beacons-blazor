import { DotNetObject } from "./DotNetObject";

export class Beacon {
    constructor(private beacon: DotNetObject) { }

    public setPosition = async (position: GeolocationPosition) => {
        const response = {
            coords: {
                longitude: position.coords.longitude,
                latitude: position.coords.latitude
            }
        };

        return await this.beacon.invokeMethodAsync("SetLatestPosition", response);
    }

    public setGeoLocationUnavailable = async () => {
        return await this.beacon.invokeMethodAsync("SetGeoLocationNotAvailable");
    }
}