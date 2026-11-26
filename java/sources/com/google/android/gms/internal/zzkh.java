package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public interface zzkh extends IInterface {
    zzju createAdLoaderBuilder(IObjectWrapper iObjectWrapper, String str, zzuq zzuqVar, int i) throws RemoteException;

    zzwx createAdOverlay(IObjectWrapper iObjectWrapper) throws RemoteException;

    zzjz createBannerAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException;

    zzxj createInAppPurchaseManager(IObjectWrapper iObjectWrapper) throws RemoteException;

    zzjz createInterstitialAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException;

    zzow createNativeAdViewDelegate(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2) throws RemoteException;

    zzacy createRewardedVideoAd(IObjectWrapper iObjectWrapper, zzuq zzuqVar, int i) throws RemoteException;

    zzjz createSearchAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, int i) throws RemoteException;

    zzkn getMobileAdsSettingsManager(IObjectWrapper iObjectWrapper) throws RemoteException;

    zzkn getMobileAdsSettingsManagerWithClientJarVersion(IObjectWrapper iObjectWrapper, int i) throws RemoteException;
}
