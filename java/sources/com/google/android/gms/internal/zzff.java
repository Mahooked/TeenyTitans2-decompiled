package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzff extends zzed implements zzfd {
    zzff(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.identifier.internal.IAdvertisingIdService");
    }

    @Override // com.google.android.gms.internal.zzfd
    public final String getId() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzfd
    public final boolean zzb(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        Parcel parcelZza = zza(2, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzfd
    public final void zzc(String str, boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, z);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzfd
    public final String zzq(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(3, parcelZzZ);
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }
}
