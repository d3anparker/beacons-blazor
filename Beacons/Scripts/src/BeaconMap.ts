import * as L from 'leaflet';

export class BeaconMap {
    private map: L.Map;
    constructor(mapId: string) {
        this.map = L.map(mapId);      
    }

    public updateMap = (lat: number, lng: number) => {
        this.map.setView([lat, lng], 13);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 13,
            attribution: '© OpenStreetMap',
        }).addTo(this.map);

        L.marker([lat, lng]).addTo(this.map);
    }
}