package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbag extends zzed implements zzbaf {
    zzbag(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.clearcut.internal.IClearcutLoggerService");
    }

    @Override // com.google.android.gms.internal.zzbaf
    public final void zza(zzbad zzbadVar, zzazu zzazuVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzbadVar);
        zzef.zza(parcelZzZ, zzazuVar);
        zzc(1, parcelZzZ);
    }
}
