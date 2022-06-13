import { Beacon } from "./Beacon.js";

export class WatchHandler {
    private readonly _options: PositionOptions = {
        enableHighAccuracy: true,
        maximumAge: 5000 // 5 seconds.
    };

    constructor(private beacon: Beacon, private navigator: Navigator) {
    }  

    startWatch = async () : Promise<number> => {
        if (!this.navigator.geolocation) {
            await this.beacon.setGeoLocationUnavailable();

            return -1;
        }

        return this.navigator.geolocation.watchPosition((position: GeolocationPosition) => this.beacon.setPosition(position), () => { }, this._options);
    }

    stopWatch = (id: number) => {
        this.navigator.geolocation.clearWatch(id);
    }
}