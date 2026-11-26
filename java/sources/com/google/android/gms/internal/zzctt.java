package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzctt extends zzed implements zzcts {
    zzctt(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.signin.internal.ISignInService");
    }

    @Override // com.google.android.gms.internal.zzcts
    public final void zza(com.google.android.gms.common.internal.zzal zzalVar, int i, boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzalVar);
        parcelZzZ.writeInt(i);
        zzef.zza(parcelZzZ, z);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcts
    public final void zza(zzctv zzctvVar, zzctq zzctqVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzctvVar);
        zzef.zza(parcelZzZ, zzctqVar);
        zzb(12, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcts
    public final void zzbv(int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        zzb(7, parcelZzZ);
    }
}
