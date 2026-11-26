package com.google.android.gms.internal;

import android.content.Intent;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzxl extends zzed implements zzxj {
    zzxl(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.purchase.client.IInAppPurchaseManager");
    }

    @Override // com.google.android.gms.internal.zzxj
    public void onActivityResult(int i, int i2, Intent intent) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        parcelZzZ.writeInt(i2);
        zzef.zza(parcelZzZ, intent);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzxj
    public final void onCreate() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzxj
    public void onDestroy() throws RemoteException {
        zzb(2, zzZ());
    }
}
