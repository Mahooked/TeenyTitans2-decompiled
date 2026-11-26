package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzada extends zzed implements zzacy {
    zzada(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.reward.client.IRewardedVideoAd");
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void destroy() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzacy
    public final String getMediationAdapterClassName() throws RemoteException {
        Parcel parcelZza = zza(12, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final boolean isLoaded() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void pause() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void resume() throws RemoteException {
        zzb(7, zzZ());
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setImmersiveMode(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(34, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setUserId(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void show() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadd zzaddVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzaddVar);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadj zzadjVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzadjVar);
        zzb(1, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzf(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzg(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(10, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzh(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(11, parcelZzZ);
    }
}
