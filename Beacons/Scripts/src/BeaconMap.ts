declare var L: any;
//import * as L from 'leaflet';

export class BeaconMap {
    constructor(private mapId: string) {
    }

    public updateMap = (lat: number, lng: number) => {
        const map = L.map(this.mapId);        
        
        map.setView([lat, lng], 13);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 13,
            attribution: '© OpenStreetMap',
        }).addTo(map);

        L.marker([lat, lng]).addTo(map);
    }
}