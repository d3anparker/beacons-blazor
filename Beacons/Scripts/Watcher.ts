import { DotNetObject } from "./DotNetObject.js";

export class Watcher {
    constructor(private watcher: DotNetObject) { }

    public setPosition = async (position: GeolocationPosition) => {
        const response = {
            coords: {
                longitude: position.coords.longitude,
                latitude: position.coords.latitude,
                accuracy: position.coords.accuracy
            }
        };

        return await this.watcher.invokeMethodAsync("SetLatestPosition", response);
    }

    public setGeoLocationUnavailable = async () => {
        return await this.watcher.invokeMethodAsync("SetGeoLocationNotAvailable");
    }

    public setGeoLocationError = async (error: GeolocationPositionError) => {
        const response = {
            message: error.message
        };

        return await this.watcher.invokeMethodAsync("SetGeoLocationError", response);
    }
}