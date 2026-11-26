package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzng extends zzed implements zzne {
    zzng(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.customrenderedad.client.ICustomRenderedAd");
    }

    @Override // com.google.android.gms.internal.zzne
    public final String getContent() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzne
    public final void recordClick() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzne
    public final void recordImpression() throws RemoteException {
        zzb(5, zzZ());
    }

    @Override // com.google.android.gms.internal.zzne
    public final String zzdX() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzne
    public final void zzi(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(3, parcelZzZ);
    }
}
