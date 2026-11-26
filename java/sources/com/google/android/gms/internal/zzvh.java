package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzvh extends zzed implements zzvf {
    zzvh(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.INativeContentAdMapper");
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getAdvertiser() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getBody() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getCallToAction() throws RemoteException {
        Parcel parcelZza = zza(6, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final Bundle getExtras() throws RemoteException {
        Parcel parcelZza = zza(13, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getHeadline() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final List getImages() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        ArrayList arrayListZzb = zzef.zzb(parcelZza);
        parcelZza.recycle();
        return arrayListZzb;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final boolean getOverrideClickHandling() throws RemoteException {
        Parcel parcelZza = zza(12, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final boolean getOverrideImpressionRecording() throws RemoteException {
        Parcel parcelZza = zza(11, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(16, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void recordImpression() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzvf
    public final zzos zzem() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        zzos zzosVarZzi = zzot.zzi(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzosVarZzi;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final IObjectWrapper zzfw() throws RemoteException {
        Parcel parcelZza = zza(15, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzl(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzm(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(10, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzn(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(14, parcelZzZ);
    }
}
