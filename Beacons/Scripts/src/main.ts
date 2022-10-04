import { BeaconMap } from "./BeaconMap";
import { DotNetObject } from "./DotNetObject";
import { Sharer } from "./Sharer";
import { Watcher } from "./Watcher";
import { WatchHandler } from "./WatchHandler";

export function createWatchHandler(watcher: DotNetObject): WatchHandler {
    return new WatchHandler(new Watcher(watcher), window.navigator);
}

export function createSharer(): Sharer {
    return new Sharer(window.navigator);
}

export function createMap(mapId: string): BeaconMap {
    return new BeaconMap(mapId);
}