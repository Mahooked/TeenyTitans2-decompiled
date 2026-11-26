package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzcay extends zzed implements zzcax {
    zzcay(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.gass.internal.IGassService");
    }

    @Override // com.google.android.gms.internal.zzcax
    public final zzcav zza(zzcat zzcatVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcatVar);
        Parcel parcelZza = zza(1, parcelZzZ);
        zzcav zzcavVar = (zzcav) zzef.zza(parcelZza, zzcav.CREATOR);
        parcelZza.recycle();
        return zzcavVar;
    }
}
