package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzaao extends zzed implements zzaam {
    zzaao(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.request.IAdRequestService");
    }

    @Override // com.google.android.gms.internal.zzaam
    public final void zza(zzaae zzaaeVar, zzaap zzaapVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzaaeVar);
        zzef.zza(parcelZzZ, zzaapVar);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaam
    public final void zza(zzaax zzaaxVar, zzaas zzaasVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzaaxVar);
        zzef.zza(parcelZzZ, zzaasVar);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaam
    public final zzaai zzc(zzaae zzaaeVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzaaeVar);
        Parcel parcelZza = zza(1, parcelZzZ);
        zzaai zzaaiVar = (zzaai) zzef.zza(parcelZza, zzaai.CREATOR);
        parcelZza.recycle();
        return zzaaiVar;
    }
}
