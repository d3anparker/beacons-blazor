import { Beacon } from './Beacon.js';
import { DotNetObject } from './DotNetObject.js';
import { WatchHandler } from './WatchHandler.js';

export function startWatch(beacon: DotNetObject) {
    const handler = new WatchHandler(new Beacon(beacon), window.navigator);

    return handler.startWatch();
}

export function stopWatch(beacon: DotNetObject, id: number) {
    const handler = new WatchHandler(new Beacon(beacon), window.navigator);

    handler.stopWatch(id);
}