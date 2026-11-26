package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzve extends zzed implements zzvc {
    zzve(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.INativeAppInstallAdMapper");
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getBody() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getCallToAction() throws RemoteException {
        Parcel parcelZza = zza(6, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final Bundle getExtras() throws RemoteException {
        Parcel parcelZza = zza(15, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getHeadline() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final List getImages() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        ArrayList arrayListZzb = zzef.zzb(parcelZza);
        parcelZza.recycle();
        return arrayListZzb;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final boolean getOverrideClickHandling() throws RemoteException {
        Parcel parcelZza = zza(14, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final boolean getOverrideImpressionRecording() throws RemoteException {
        Parcel parcelZza = zza(13, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getPrice() throws RemoteException {
        Parcel parcelZza = zza(9, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final double getStarRating() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        double d = parcelZza.readDouble();
        parcelZza.recycle();
        return d;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getStore() throws RemoteException {
        Parcel parcelZza = zza(8, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(17, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void recordImpression() throws RemoteException {
        zzb(10, zzZ());
    }

    @Override // com.google.android.gms.internal.zzvc
    public final zzos zzeh() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        zzos zzosVarZzi = zzot.zzi(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzosVarZzi;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final IObjectWrapper zzfw() throws RemoteException {
        Parcel parcelZza = zza(18, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzl(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(11, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzm(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(12, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzn(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(16, parcelZzZ);
    }
}
