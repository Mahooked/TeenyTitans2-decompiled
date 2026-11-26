package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzuy extends zzed implements zzuw {
    zzuy(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdClicked() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdClosed() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdFailedToLoad(int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdImpression() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdLeftApplication() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdLoaded() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdOpened() throws RemoteException {
        zzb(5, zzZ());
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAppEvent(String str, String str2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void zza(zzuz zzuzVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzuzVar);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void zzb(zzpj zzpjVar, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzpjVar);
        parcelZzZ.writeString(str);
        zzb(10, parcelZzZ);
    }
}
