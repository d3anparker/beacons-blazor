window.startWatch = (beacon) => {
    const handler = new WatchHandler(beacon);

    handler.startWatch();   
}

class WatchHandler {
    constructor(beacon) {
        this.beacon = beacon;
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

    startWatch = async () => {
        if (!navigator.geolocation) {
            return await this.setGeoLocationUnavailable();
        }

        navigator.geolocation.watchPosition((position) => this.setPosition(position));
    }
}