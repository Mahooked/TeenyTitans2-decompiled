package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzjq extends zzed implements zzjo {
    zzjq(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdListener");
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClicked() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClosed() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdFailedToLoad(int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdImpression() throws RemoteException {
        zzb(7, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLeftApplication() throws RemoteException {
        zzb(3, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLoaded() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdOpened() throws RemoteException {
        zzb(5, zzZ());
    }
}
