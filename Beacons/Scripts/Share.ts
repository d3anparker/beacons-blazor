export async function share(shareData: ShareData): Promise<void> {
    await navigator.share(shareData);
}