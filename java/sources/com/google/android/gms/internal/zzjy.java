package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzjy extends zzed implements zzjx {
    zzjy(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdLoaderBuilderCreator");
    }

    @Override // com.google.android.gms.internal.zzjx
    public final IBinder zza(IObjectWrapper iObjectWrapper, String str, zzuq zzuqVar, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(11020000);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
