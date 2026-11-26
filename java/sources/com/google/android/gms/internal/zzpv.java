package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzpv extends zzed implements zzpt {
    zzpv(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.IOnCustomClickListener");
    }

    @Override // com.google.android.gms.internal.zzpt
    public final void zzb(zzpj zzpjVar, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzpjVar);
        parcelZzZ.writeString(str);
        zzb(1, parcelZzZ);
    }
}
