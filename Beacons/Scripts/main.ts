import { DotNetObject } from "./DotNetObject.js";
import { Sharer } from "./Sharer.js";
import { Watcher } from "./Watcher.js";
import { WatchHandler } from "./WatchHandler.js";

export function createWatchHandler(watcher: DotNetObject): WatchHandler {
    return new WatchHandler(new Watcher(watcher), window.navigator);
}

export function createSharer(): Sharer {
    return new Sharer(window.navigator);
}