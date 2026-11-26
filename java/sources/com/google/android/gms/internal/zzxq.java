package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzxq extends zzed implements zzxo {
    zzxq(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.purchase.client.IPlayStorePurchaseListener");
    }

    @Override // com.google.android.gms.internal.zzxo
    public final void zza(zzxm zzxmVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzxmVar);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzxo
    public final boolean zzas(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(1, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }
}
