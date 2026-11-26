package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzoy extends zzed implements zzow {
    zzoy(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeAdViewDelegate");
    }

    @Override // com.google.android.gms.internal.zzow
    public final void destroy() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzow
    public final IObjectWrapper zzL(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(2, parcelZzZ);
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zzb(IObjectWrapper iObjectWrapper, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeInt(i);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zzd(String str, IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(1, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zze(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(3, parcelZzZ);
    }
}
