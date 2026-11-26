package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzfc extends zzed implements zzfb {
    zzfc(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.adshield.internal.IAdShieldCreator");
    }

    @Override // com.google.android.gms.internal.zzfb
    public final IBinder zza(String str, IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }

    @Override // com.google.android.gms.internal.zzfb
    public final IBinder zzb(String str, IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(2, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
