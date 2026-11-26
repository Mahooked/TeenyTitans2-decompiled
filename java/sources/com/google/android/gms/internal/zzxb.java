package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzxb extends zzed implements zzxa {
    zzxb(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.overlay.client.IAdOverlayCreator");
    }

    @Override // com.google.android.gms.internal.zzxa
    public final IBinder zzp(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        parcelZza.recycle();
        return strongBinder;
    }
}
