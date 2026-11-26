package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzpa extends zzed implements zzoz {
    zzpa(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeAdViewDelegateCreator");
    }

    @Override // com.google.android.gms.internal.zzoz
    public final IBinder zza(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2, IObjectWrapper iObjectWrapper3, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, iObjectWrapper2);
        zzef.zza(parcelZzZ, iObjectWrapper3);
        parcelZzZ.writeInt(11020000);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
