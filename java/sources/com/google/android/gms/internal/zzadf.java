package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzadf extends zzed implements zzadd {
    zzadf(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.reward.client.IRewardedVideoAdListener");
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdClosed() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdFailedToLoad(int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLeftApplication() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLoaded() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdOpened() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoStarted() throws RemoteException {
        zzb(3, zzZ());
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void zza(zzacv zzacvVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzacvVar);
        zzb(5, parcelZzZ);
    }
}
