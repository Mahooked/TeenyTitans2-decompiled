package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzaec extends zzed implements zzaea {
    zzaec(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.reward.mediation.client.IMediationRewardedVideoAdListener");
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zza(IObjectWrapper iObjectWrapper, zzaee zzaeeVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzaeeVar);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzc(IObjectWrapper iObjectWrapper, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeInt(i);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzd(IObjectWrapper iObjectWrapper, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeInt(i);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzq(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(1, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzr(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzs(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzt(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzu(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(6, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzv(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(8, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzw(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(10, parcelZzZ);
    }
}
