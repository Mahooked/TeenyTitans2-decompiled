package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsy extends zzade {
    private /* synthetic */ zzsj zzKb;

    zzsy(zzsj zzsjVar) {
        this.zzKb = zzsjVar;
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdClosed() throws RemoteException {
        this.zzKb.zztH.add(new zztc(this));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdFailedToLoad(int i) throws RemoteException {
        this.zzKb.zztH.add(new zztf(this, i));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLeftApplication() throws RemoteException {
        this.zzKb.zztH.add(new zzte(this));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLoaded() throws RemoteException {
        this.zzKb.zztH.add(new zzsz(this));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdOpened() throws RemoteException {
        this.zzKb.zztH.add(new zzta(this));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoStarted() throws RemoteException {
        this.zzKb.zztH.add(new zztb(this));
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void zza(zzacv zzacvVar) throws RemoteException {
        this.zzKb.zztH.add(new zztd(this, zzacvVar));
    }
}
