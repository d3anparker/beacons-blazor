import { Beacon } from './Beacon.js';
import { DotNetObject } from './DotNetObject.js';
import { WatchHandler } from './WatchHandler.js';

let handler: WatchHandler

export function initialise(beacon: DotNetObject) {
    handler = new WatchHandler(new Beacon(beacon), window.navigator);
}

export function startWatch() {
    if (!handler) {
        throw Error("Module not initialised");
    }

    return handler.startWatch();
}

export function stopWatch(id: number) {
    if (!handler) {
        throw Error("Module not initialised");
    }

    handler.stopWatch(id);
}