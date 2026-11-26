package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzxi extends zzed implements zzxg {
    zzxi(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.purchase.client.IInAppPurchaseListener");
    }

    @Override // com.google.android.gms.internal.zzxg
    public final void zza(zzxe zzxeVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzxeVar);
        zzb(1, parcelZzZ);
    }
}
