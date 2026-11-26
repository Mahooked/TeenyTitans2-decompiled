package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzus extends zzed implements zzuq {
    zzus(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.IAdapterCreator");
    }

    @Override // com.google.android.gms.internal.zzuq
    public final zzut zzah(String str) throws RemoteException {
        zzut zzuvVar;
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzuvVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapter");
            zzuvVar = iInterfaceQueryLocalInterface instanceof zzut ? (zzut) iInterfaceQueryLocalInterface : new zzuv(strongBinder);
        }
        parcelZza.recycle();
        return zzuvVar;
    }

    @Override // com.google.android.gms.internal.zzuq
    public final boolean zzai(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(2, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }
}
