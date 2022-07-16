import { Watcher } from './Watcher.js';
import { DotNetObject } from './DotNetObject.js';
import { WatchHandler } from './WatchHandler.js';

let handler: WatchHandler

export function initialise(watcher: DotNetObject) {
    handler = new WatchHandler(new Watcher(watcher), window.navigator);
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