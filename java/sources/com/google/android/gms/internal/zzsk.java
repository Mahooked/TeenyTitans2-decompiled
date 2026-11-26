package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsk extends zzjp {
    private /* synthetic */ zzsj zzKb;

    zzsk(zzsj zzsjVar) {
        this.zzKb = zzsjVar;
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClicked() throws RemoteException {
        this.zzKb.zztH.add(new zzsr(this));
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClosed() throws RemoteException {
        this.zzKb.zztH.add(new zzsl(this));
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdFailedToLoad(int i) throws RemoteException {
        this.zzKb.zztH.add(new zzsm(this, i));
        zzafr.v("Pooled interstitial failed to load.");
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdImpression() throws RemoteException {
        this.zzKb.zztH.add(new zzsq(this));
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLeftApplication() throws RemoteException {
        this.zzKb.zztH.add(new zzsn(this));
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLoaded() throws RemoteException {
        this.zzKb.zztH.add(new zzso(this));
        zzafr.v("Pooled interstitial loaded.");
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdOpened() throws RemoteException {
        this.zzKb.zztH.add(new zzsp(this));
    }
}
