package com.google.android.gms.internal;

import com.google.android.gms.ads.AdListener;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzio extends zzjp {
    private final AdListener zzzM;

    public zzio(AdListener adListener) {
        this.zzzM = adListener;
    }

    public final AdListener getAdListener() {
        return this.zzzM;
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClicked() {
        this.zzzM.onAdClicked();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClosed() {
        this.zzzM.onAdClosed();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdFailedToLoad(int i) {
        this.zzzM.onAdFailedToLoad(i);
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdImpression() {
        this.zzzM.onAdImpression();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLeftApplication() {
        this.zzzM.onAdLeftApplication();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLoaded() {
        this.zzzM.onAdLoaded();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdOpened() {
        this.zzzM.onAdOpened();
    }
}
