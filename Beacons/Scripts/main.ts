import { Beacon } from './Beacon';
import { DotNetObject } from './DotNetObject';
import { WatchHandler } from './WatchHandler';

(window as any).startWatch = (beacon: DotNetObject) => {
    const handler = new WatchHandler(new Beacon(beacon), window.navigator);

    return handler.startWatch();   
}

(window as any).stopWatch = (beacon: DotNetObject, id: number) => {
    const handler = new WatchHandler(new Beacon(beacon), window.navigator);

    handler.stopWatch(id);
}