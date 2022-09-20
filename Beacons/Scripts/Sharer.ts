export class Sharer {
    constructor(private navigator: Navigator) {
    }

    public share = async (shareData: ShareData) : Promise<void> => {
        await this.navigator.share(shareData);
    }
}