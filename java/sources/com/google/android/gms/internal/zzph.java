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
public final class zzph extends zzed implements zzpf {
    zzph(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeContentAd");
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void destroy() throws RemoteException {
        zzb(10, zzZ());
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getAdvertiser() throws RemoteException {
        Parcel parcelZza = zza(8, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getBody() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getCallToAction() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final Bundle getExtras() throws RemoteException {
        Parcel parcelZza = zza(9, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getHeadline() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpf, com.google.android.gms.internal.zzob
    public final List getImages() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        ArrayList arrayListZzb = zzef.zzb(parcelZza);
        parcelZza.recycle();
        return arrayListZzb;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(11, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void zzc(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        zzb(12, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzpf
    public final boolean zzd(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        Parcel parcelZza = zza(13, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void zze(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        zzb(14, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzpf
    public final IObjectWrapper zzei() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final zzos zzem() throws RemoteException {
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
}
