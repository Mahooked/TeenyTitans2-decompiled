package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzjt extends zzed implements zzjr {
    zzjt(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdLoader");
    }

    @Override // com.google.android.gms.internal.zzjr
    public final String getMediationAdapterClassName() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final boolean isLoading() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final String zzaI() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final void zzc(zzir zzirVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzirVar);
        zzb(1, parcelZzZ);
    }
}
