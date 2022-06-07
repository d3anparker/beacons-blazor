import { WatchHandler } from './WatchHandler';

(window as any).startWatch = (beacon) => {
    const handler = new WatchHandler(beacon, window.navigator);

    return handler.startWatch();   
}

(window as any).stopWatch = (beacon, id) => {
    const handler = new WatchHandler(beacon, window.navigator);

    handler.stopWatch(id);
}