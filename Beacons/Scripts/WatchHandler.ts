import { Beacon } from "./Beacon.js";

export class WatchHandler {
    private readonly _options: PositionOptions = {
        enableHighAccuracy: true,
        maximumAge: 5000 // 5 seconds.
    };

    constructor(private beacon: Beacon, private navigator: Navigator) {
    }  

    public startWatch = async () : Promise<number> => {
        if (!this.navigator.geolocation) {
            await this.beacon.setGeoLocationUnavailable();

            return -1;
        }

        return this.navigator.geolocation.watchPosition(this.setPosition,
            () => { },
            this._options);
    }

    public stopWatch = (id: number) => {
        this.navigator.geolocation.clearWatch(id);
    }

    private setPosition = (position: GeolocationPosition) => this.beacon.setPosition(position);
}