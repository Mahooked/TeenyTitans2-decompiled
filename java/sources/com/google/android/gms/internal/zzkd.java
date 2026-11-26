package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzkd extends zzed implements zzkc {
    zzkd(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdManagerCreator");
    }

    @Override // com.google.android.gms.internal.zzkc
    public final IBinder zza(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i, int i2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(11020000);
        parcelZzZ.writeInt(i2);
        Parcel parcelZza = zza(2, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
