package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzkb extends zzed implements zzjz {
    zzkb(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdManager");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void destroy() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String getAdUnitId() throws RemoteException {
        Parcel parcelZza = zza(31, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String getMediationAdapterClassName() throws RemoteException {
        Parcel parcelZza = zza(18, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzks getVideoController() throws RemoteException {
        zzks zzkuVar;
        Parcel parcelZza = zza(26, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkuVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IVideoController");
            zzkuVar = iInterfaceQueryLocalInterface instanceof zzks ? (zzks) iInterfaceQueryLocalInterface : new zzku(strongBinder);
        }
        parcelZza.recycle();
        return zzkuVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isLoading() throws RemoteException {
        Parcel parcelZza = zza(23, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isReady() throws RemoteException {
        Parcel parcelZza = zza(3, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void pause() throws RemoteException {
        zzb(5, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void resume() throws RemoteException {
        zzb(6, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setImmersiveMode(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(34, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setManualImpressionsEnabled(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(22, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setUserId(String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzb(25, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void showInterstitial() throws RemoteException {
        zzb(9, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void stopLoading() throws RemoteException {
        zzb(10, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzadd zzaddVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzaddVar);
        zzb(24, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zziv zzivVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzivVar);
        zzb(13, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjl zzjlVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzjlVar);
        zzb(20, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjo zzjoVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzjoVar);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzke zzkeVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzkeVar);
        zzb(8, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzkk zzkkVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzkkVar);
        zzb(21, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzky zzkyVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzkyVar);
        zzb(30, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzlx zzlxVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzlxVar);
        zzb(29, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zznhVar);
        zzb(19, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxg zzxgVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzxgVar);
        zzb(14, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxo zzxoVar, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzxoVar);
        parcelZzZ.writeString(str);
        zzb(15, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean zza(zzir zzirVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzirVar);
        Parcel parcelZza = zza(4, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String zzaI() throws RemoteException {
        Parcel parcelZza = zza(35, zzZ());
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final IObjectWrapper zzal() throws RemoteException {
        Parcel parcelZza = zza(1, zzZ());
        IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return iObjectWrapperZzM;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zziv zzam() throws RemoteException {
        Parcel parcelZza = zza(12, zzZ());
        zziv zzivVar = (zziv) zzef.zza(parcelZza, zziv.CREATOR);
        parcelZza.recycle();
        return zzivVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zzao() throws RemoteException {
        zzb(11, zzZ());
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzke zzax() throws RemoteException {
        zzke zzkgVar;
        Parcel parcelZza = zza(32, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkgVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAppEventListener");
            zzkgVar = iInterfaceQueryLocalInterface instanceof zzke ? (zzke) iInterfaceQueryLocalInterface : new zzkg(strongBinder);
        }
        parcelZza.recycle();
        return zzkgVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzjo zzay() throws RemoteException {
        zzjo zzjqVar;
        Parcel parcelZza = zza(33, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzjqVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdListener");
            zzjqVar = iInterfaceQueryLocalInterface instanceof zzjo ? (zzjo) iInterfaceQueryLocalInterface : new zzjq(strongBinder);
        }
        parcelZza.recycle();
        return zzjqVar;
    }
}
