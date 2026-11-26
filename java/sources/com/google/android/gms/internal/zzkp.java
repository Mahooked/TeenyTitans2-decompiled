package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzkp extends zzed implements zzkn {
    zzkp(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IMobileAdsSettingManager");
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void initialize() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void setAppMuted(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void setAppVolume(float f) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeFloat(f);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzb(IObjectWrapper iObjectWrapper, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeString(str);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzc(String str, IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(6, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzu(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzb(3, parcelZzZ);
    }
}
