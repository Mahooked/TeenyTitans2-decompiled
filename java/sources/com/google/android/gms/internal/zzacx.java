package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzacx extends zzed implements zzacv {
    zzacx(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.reward.client.IRewardItem");
    }

    @Override // com.google.android.gms.internal.zzacv
    public final int getAmount() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        int i = parcelZza.readInt();
        parcelZza.recycle();
        return i;
    }

    @Override // com.google.android.gms.internal.zzacv
    public final String getType() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }
}
