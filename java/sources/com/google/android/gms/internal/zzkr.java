package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzkr extends zzed implements zzkq {
    zzkr(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IMobileAdsSettingManagerCreator");
    }

    @Override // com.google.android.gms.internal.zzkq
    public final IBinder zza(IObjectWrapper iObjectWrapper, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeInt(11020000);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
