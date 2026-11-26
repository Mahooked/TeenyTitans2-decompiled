package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzki extends zzee implements zzkh {
    public zzki() {
        attachInterface(this, "com.google.android.gms.ads.internal.client.IClientApi");
    }

    public static zzkh asInterface(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IClientApi");
        return iInterfaceQueryLocalInterface instanceof zzkh ? (zzkh) iInterfaceQueryLocalInterface : new zzkj(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zzjz zzjzVarCreateBannerAdManager = createBannerAdManager(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), (zziv) zzef.zza(parcel, zziv.CREATOR), parcel.readString(), zzur.zzq(parcel.readStrongBinder()), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjzVarCreateBannerAdManager);
                break;
            case 2:
                zzjz zzjzVarCreateInterstitialAdManager = createInterstitialAdManager(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), (zziv) zzef.zza(parcel, zziv.CREATOR), parcel.readString(), zzur.zzq(parcel.readStrongBinder()), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjzVarCreateInterstitialAdManager);
                break;
            case 3:
                zzju zzjuVarCreateAdLoaderBuilder = createAdLoaderBuilder(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.readString(), zzur.zzq(parcel.readStrongBinder()), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjuVarCreateAdLoaderBuilder);
                break;
            case 4:
                zzkn mobileAdsSettingsManager = getMobileAdsSettingsManager(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, mobileAdsSettingsManager);
                break;
            case 5:
                zzow zzowVarCreateNativeAdViewDelegate = createNativeAdViewDelegate(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zzowVarCreateNativeAdViewDelegate);
                break;
            case 6:
                zzacy zzacyVarCreateRewardedVideoAd = createRewardedVideoAd(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), zzur.zzq(parcel.readStrongBinder()), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzacyVarCreateRewardedVideoAd);
                break;
            case 7:
                zzxj zzxjVarCreateInAppPurchaseManager = createInAppPurchaseManager(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zzxjVarCreateInAppPurchaseManager);
                break;
            case 8:
                zzwx zzwxVarCreateAdOverlay = createAdOverlay(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zzwxVarCreateAdOverlay);
                break;
            case 9:
                zzkn mobileAdsSettingsManagerWithClientJarVersion = getMobileAdsSettingsManagerWithClientJarVersion(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, mobileAdsSettingsManagerWithClientJarVersion);
                break;
            case 10:
                zzjz zzjzVarCreateSearchAdManager = createSearchAdManager(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), (zziv) zzef.zza(parcel, zziv.CREATOR), parcel.readString(), parcel.readInt());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjzVarCreateSearchAdManager);
                break;
            default:
                return false;
        }
        return true;
    }
}
