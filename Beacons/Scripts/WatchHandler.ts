import { DotNetObject } from "./DotNetObject";

export class WatchHandler {

    constructor(private beacon: DotNetObject, private navigator: Navigator) {
    }
    
    setPosition = async (position) => {
        const response = {
            coords: {
                longitude: position.coords.longitude,
                latitude: position.coords.latitude
            }
        };

        return await this.beacon.invokeMethodAsync("SetLatestPosition", response);
    }

    setGeoLocationUnavailable = async () => {
        return await this.beacon.invokeMethodAsync("SetGeoLocationNotAvailable");
    }

    startWatch = async () : Promise<number> => {
        if (!this.navigator.geolocation) {
            await this.setGeoLocationUnavailable();

            return -1;
        }

        const options: PositionOptions = {
            enableHighAccuracy: true,
            maximumAge: 5000 // 5 seconds.
        };

        return this.navigator.geolocation.watchPosition((position) => this.setPosition(position), () => { }, options);
    }

    stopWatch = (id) => {
        this.navigator.geolocation.clearWatch(id);
    }
}

