import { Watcher } from "./Watcher.js";

export class WatchHandler {
    private readonly _options: PositionOptions = {
        enableHighAccuracy: true,
        maximumAge: 5000 // 5 seconds.
    };

    constructor(private watcher: Watcher, private navigator: Navigator) {
    }  

    public startWatch = async () : Promise<number> => {
        if (!this.navigator.geolocation) {
            await this.watcher.setGeoLocationUnavailable();

            return -1;
        }

        return this.navigator.geolocation.watchPosition(this.setPosition,
            this.setPositionError,
            this._options);
    }

    public stopWatch = (id: number) => {
        this.navigator.geolocation.clearWatch(id);
    }

    private setPosition = (position: GeolocationPosition) => this.watcher.setPosition(position);
    private setPositionError = (error: GeolocationPositionError) => this.watcher.setGeoLocationError(error);
}