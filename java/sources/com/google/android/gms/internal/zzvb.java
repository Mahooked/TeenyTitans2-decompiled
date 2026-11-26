package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzvb extends zzed implements zzuz {
    zzvb(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.IMediationResponseMetadata");
    }

    @Override // com.google.android.gms.internal.zzuz
    public final int zzfo() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        int i = parcelZza.readInt();
        parcelZza.recycle();
        return i;
    }
}
