package com.google.android.gms.internal;

import android.net.Uri;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzou extends zzed implements zzos {
    zzou(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeAdImage");
    }

    @Override // com.google.android.gms.internal.zzos
    public final double getScale() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        double d = parcelZza.readDouble();
        parcelZza.recycle();
        return d;
    }

    @Override // com.google.android.gms.internal.zzos
    public final Uri getUri() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        Uri uri = (Uri) zzef.zza(parcelZza, Uri.CREATOR);
        parcelZza.recycle();
        return uri;
    }

    @Override // com.google.android.gms.internal.zzos
    public final IObjectWrapper zzeg() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }
}
