package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzpd extends zzed implements zzpb {
    zzpd(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeAppInstallAd");
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void destroy() throws RemoteException {
        zzb(12, zzZ());
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getBody() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getCallToAction() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final Bundle getExtras() throws RemoteException {
        Parcel parcelZza = zza(11, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getHeadline() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpb, com.google.android.gms.internal.zzob
    public final List getImages() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        ArrayList arrayListZzb = zzef.zzb(parcelZza);
        parcelZza.recycle();
        return arrayListZzb;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getPrice() throws RemoteException {
        Parcel parcelZza = zza(10, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final double getStarRating() throws RemoteException {
        Parcel parcelZza = zza(8, zzZ());
        double d = parcelZza.readDouble();
        parcelZza.recycle();
        return d;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getStore() throws RemoteException {
        Parcel parcelZza = zza(9, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(13, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void zzc(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        zzb(14, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzpb
    public final boolean zzd(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        Parcel parcelZza = zza(15, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void zze(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        zzb(16, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzpb
    public final zzos zzeh() throws RemoteException {
        zzos zzouVar;
        Parcel parcelZza = zza(6, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzouVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeAdImage");
            zzouVar = iInterfaceQueryLocalInterface instanceof zzos ? (zzos) iInterfaceQueryLocalInterface : new zzou(strongBinder);
        }
        parcelZza.recycle();
        return zzouVar;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final IObjectWrapper zzei() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }
}
