package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzie extends zzed implements zzid {
    zzie(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.cache.ICacheService");
    }

    @Override // com.google.android.gms.internal.zzid
    public final zzhx zza(zzia zziaVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zziaVar);
        Parcel parcelZza = zza(1, parcelZzZ);
        zzhx zzhxVar = (zzhx) zzef.zza(parcelZza, zzhx.CREATOR);
        parcelZza.recycle();
        return zzhxVar;
    }
}
