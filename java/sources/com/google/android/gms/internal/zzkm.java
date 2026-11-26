package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzkm extends zzed implements zzkk {
    zzkm(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.ICorrelationIdProvider");
    }

    @Override // com.google.android.gms.internal.zzkk
    public final long getValue() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        long j = parcelZza.readLong();
        parcelZza.recycle();
        return j;
    }
}
