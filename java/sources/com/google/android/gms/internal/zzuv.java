package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzuv extends zzed implements zzut {
    zzuv(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.mediation.client.IMediationAdapter");
    }

    @Override // com.google.android.gms.internal.zzut
    public final void destroy() throws RemoteException {
        zzb(5, zzZ());
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle getInterstitialAdapterInfo() throws RemoteException {
        Parcel parcelZza = zza(18, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzks getVideoController() throws RemoteException {
        Parcel parcelZza = zza(26, zzZ());
        zzks zzksVarZzg = zzkt.zzg(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzksVarZzg;
    }

    @Override // com.google.android.gms.internal.zzut
    public final IObjectWrapper getView() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean isInitialized() throws RemoteException {
        Parcel parcelZza = zza(13, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzut
    public final void pause() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzut
    public final void resume() throws RemoteException {
        zzb(9, zzZ());
    }

    @Override // com.google.android.gms.internal.zzut
    public final void setImmersiveMode(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(25, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showInterstitial() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showVideo() throws RemoteException {
        zzb(12, zzZ());
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzaea zzaeaVar, List<String> list) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzaeaVar);
        parcelZzZ.writeStringList(list);
        zzb(23, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzaea zzaeaVar, String str2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzaeaVar);
        parcelZzZ.writeString(str2);
        zzb(10, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuwVar);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        zzef.zza(parcelZzZ, zzuwVar);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar, zzon zzonVar, List<String> list) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        zzef.zza(parcelZzZ, zzuwVar);
        zzef.zza(parcelZzZ, zzonVar);
        parcelZzZ.writeStringList(list);
        zzb(14, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuwVar);
        zzb(1, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        zzef.zza(parcelZzZ, zzuwVar);
        zzb(6, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(zzir zzirVar, String str, String str2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        zzb(20, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzc(zzir zzirVar, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzirVar);
        parcelZzZ.writeString(str);
        zzb(11, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvc zzfq() throws RemoteException {
        zzvc zzveVar;
        Parcel parcelZza = zza(15, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzveVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.INativeAppInstallAdMapper");
            zzveVar = iInterfaceQueryLocalInterface instanceof zzvc ? (zzvc) iInterfaceQueryLocalInterface : new zzve(strongBinder);
        }
        parcelZza.recycle();
        return zzveVar;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvf zzfr() throws RemoteException {
        zzvf zzvhVar;
        Parcel parcelZza = zza(16, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzvhVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.INativeContentAdMapper");
            zzvhVar = iInterfaceQueryLocalInterface instanceof zzvf ? (zzvf) iInterfaceQueryLocalInterface : new zzvh(strongBinder);
        }
        parcelZza.recycle();
        return zzvhVar;
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzfs() throws RemoteException {
        Parcel parcelZza = zza(17, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzft() throws RemoteException {
        Parcel parcelZza = zza(19, zzZ());
        Bundle bundle = (Bundle) zzef.zza(parcelZza, Bundle.CREATOR);
        parcelZza.recycle();
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean zzfu() throws RemoteException {
        Parcel parcelZza = zza(22, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzpj zzfv() throws RemoteException {
        Parcel parcelZza = zza(24, zzZ());
        zzpj zzpjVarZzk = zzpk.zzk(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzpjVarZzk;
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzk(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(21, parcelZzZ);
    }
}
