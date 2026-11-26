package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzed;

/* JADX INFO: loaded from: classes.dex */
public final class zzat extends zzed implements zzar {
    zzat(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.common.internal.ICertData");
    }

    @Override // com.google.android.gms.common.internal.zzar
    public final IObjectWrapper zzoY() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.common.internal.zzar
    public final int zzoZ() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        int i = parcelZza.readInt();
        parcelZza.recycle();
        return i;
    }
}
