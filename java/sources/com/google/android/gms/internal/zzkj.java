package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzkj extends zzed implements zzkh {
    zzkj(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IClientApi");
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzju createAdLoaderBuilder(IObjectWrapper iObjectWrapper, String str, zzuq zzuqVar, int i) throws RemoteException {
        zzju zzjwVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(3, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzjwVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdLoaderBuilder");
            zzjwVar = iInterfaceQueryLocalInterface instanceof zzju ? (zzju) iInterfaceQueryLocalInterface : new zzjw(strongBinder);
        }
        parcelZza.recycle();
        return zzjwVar;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzwx createAdOverlay(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(8, parcelZzZ);
        zzwx zzwxVarZzr = zzwy.zzr(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzwxVarZzr;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzjz createBannerAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException {
        zzjz zzkbVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(1, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkbVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManager");
            zzkbVar = iInterfaceQueryLocalInterface instanceof zzjz ? (zzjz) iInterfaceQueryLocalInterface : new zzkb(strongBinder);
        }
        parcelZza.recycle();
        return zzkbVar;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzxj createInAppPurchaseManager(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(7, parcelZzZ);
        zzxj zzxjVarZzt = zzxk.zzt(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzxjVarZzt;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzjz createInterstitialAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException {
        zzjz zzkbVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(2, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkbVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManager");
            zzkbVar = iInterfaceQueryLocalInterface instanceof zzjz ? (zzjz) iInterfaceQueryLocalInterface : new zzkb(strongBinder);
        }
        parcelZza.recycle();
        return zzkbVar;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzow createNativeAdViewDelegate(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, iObjectWrapper2);
        Parcel parcelZza = zza(5, parcelZzZ);
        zzow zzowVarZzj = zzox.zzj(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzowVarZzj;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzacy createRewardedVideoAd(IObjectWrapper iObjectWrapper, zzuq zzuqVar, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzuqVar);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(6, parcelZzZ);
        zzacy zzacyVarZzv = zzacz.zzv(parcelZza.readStrongBinder());
        parcelZza.recycle();
        return zzacyVarZzv;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzjz createSearchAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, int i) throws RemoteException {
        zzjz zzkbVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzef.zza(parcelZzZ, zzivVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(10, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkbVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManager");
            zzkbVar = iInterfaceQueryLocalInterface instanceof zzjz ? (zzjz) iInterfaceQueryLocalInterface : new zzkb(strongBinder);
        }
        parcelZza.recycle();
        return zzkbVar;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzkn getMobileAdsSettingsManager(IObjectWrapper iObjectWrapper) throws RemoteException {
        zzkn zzkpVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        Parcel parcelZza = zza(4, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkpVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IMobileAdsSettingManager");
            zzkpVar = iInterfaceQueryLocalInterface instanceof zzkn ? (zzkn) iInterfaceQueryLocalInterface : new zzkp(strongBinder);
        }
        parcelZza.recycle();
        return zzkpVar;
    }

    @Override // com.google.android.gms.internal.zzkh
    public final zzkn getMobileAdsSettingsManagerWithClientJarVersion(IObjectWrapper iObjectWrapper, int i) throws RemoteException {
        zzkn zzkpVar;
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(9, parcelZzZ);
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzkpVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IMobileAdsSettingManager");
            zzkpVar = iInterfaceQueryLocalInterface instanceof zzkn ? (zzkn) iInterfaceQueryLocalInterface : new zzkp(strongBinder);
        }
        parcelZza.recycle();
        return zzkpVar;
    }
}
