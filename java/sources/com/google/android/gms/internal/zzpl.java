package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzpl extends zzed implements zzpj {
    zzpl(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.formats.client.INativeCustomTemplateAd");
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void destroy() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzpj
    public final List<String> getAvailableAssetNames() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        ArrayList<String> arrayListCreateStringArrayList = parcelZza.createStringArrayList();
        parcelZza.recycle();
        return arrayListCreateStringArrayList;
    }

    @Override // com.google.android.gms.internal.zzpj, com.google.android.gms.internal.zzoa
    public final String getCustomTemplateId() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void performClick(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void recordImpression() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzpj
    public final String zzP(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(1, parcelZzZ);
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final zzos zzQ(String str) throws RemoteException {
        zzos zzouVar;
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        Parcel parcelZza = zza(2, parcelZzZ);
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

    @Override // com.google.android.gms.internal.zzpj
    public final IObjectWrapper zzei() throws RemoteException {
        Parcel parcelZza = zza(11, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final IObjectWrapper zzen() throws RemoteException {
        Parcel parcelZza = zza(9, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final boolean zzj(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(10, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }
}
