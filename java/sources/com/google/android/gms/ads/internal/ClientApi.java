package com.google.android.gms.ads.internal;

import android.app.Activity;
import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.Keep;
import android.support.annotation.Nullable;
import android.widget.FrameLayout;
import com.google.android.gms.common.annotation.KeepForSdkWithMembers;
import com.google.android.gms.common.util.DynamiteApi;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzacr;
import com.google.android.gms.internal.zzacy;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzju;
import com.google.android.gms.internal.zzjz;
import com.google.android.gms.internal.zzki;
import com.google.android.gms.internal.zzkn;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzop;
import com.google.android.gms.internal.zzow;
import com.google.android.gms.internal.zztq;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzwx;
import com.google.android.gms.internal.zzxj;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@Keep
@DynamiteApi
@KeepForSdkWithMembers
@zzzn
public class ClientApi extends zzki {
    @Override // com.google.android.gms.internal.zzkh
    public zzju createAdLoaderBuilder(IObjectWrapper iObjectWrapper, String str, zzuq zzuqVar, int i) {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzbs.zzbz();
        return new zzak(context, str, zzuqVar, new zzaje(11020000, i, true, zzagz.zzO(context)), zzv.zzaQ());
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzwx createAdOverlay(IObjectWrapper iObjectWrapper) {
        return new com.google.android.gms.ads.internal.overlay.zzm((Activity) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzjz createBannerAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzbs.zzbz();
        return new zzx(context, zzivVar, str, zzuqVar, new zzaje(11020000, i, true, zzagz.zzO(context)), zzv.zzaQ());
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzxj createInAppPurchaseManager(IObjectWrapper iObjectWrapper) {
        return null;
    }

    /* JADX WARN: Code duplicated, block: B:6:0x0037 A[DONT_INVERT] */
    /* JADX WARN: Code duplicated, block: B:7:0x0039  */
    /* JADX WARN: Code duplicated, block: B:9:0x004b  */
    @Override // com.google.android.gms.internal.zzkh
    public zzjz createInterstitialAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, zzuq zzuqVar, int i) throws RemoteException {
        boolean z;
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzmo.initialize(context);
        zzbs.zzbz();
        zzaje zzajeVar = new zzaje(11020000, i, true, zzagz.zzO(context));
        boolean zEquals = "reward_mb".equals(zzivVar.zzAs);
        if (zEquals) {
            if (zEquals) {
                if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDZ)).booleanValue()) {
                }
            }
        } else {
            if (!((Boolean) zzbs.zzbL().zzd(zzmo.zzDY)).booleanValue()) {
                if (zEquals) {
                    z = ((Boolean) zzbs.zzbL().zzd(zzmo.zzDZ)).booleanValue();
                }
            }
        }
        return z ? new zztq(context, str, zzuqVar, zzajeVar, zzv.zzaQ()) : new zzal(context, zzivVar, str, zzuqVar, zzajeVar, zzv.zzaQ());
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzow createNativeAdViewDelegate(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2) {
        return new zzop((FrameLayout) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), (FrameLayout) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper2));
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzacy createRewardedVideoAd(IObjectWrapper iObjectWrapper, zzuq zzuqVar, int i) {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzbs.zzbz();
        return new zzacr(context, zzv.zzaQ(), zzuqVar, new zzaje(11020000, i, true, zzagz.zzO(context)));
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzjz createSearchAdManager(IObjectWrapper iObjectWrapper, zziv zzivVar, String str, int i) throws RemoteException {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzbs.zzbz();
        return new zzbm(context, zzivVar, str, new zzaje(11020000, i, true, zzagz.zzO(context)));
    }

    @Override // com.google.android.gms.internal.zzkh
    @Nullable
    public zzkn getMobileAdsSettingsManager(IObjectWrapper iObjectWrapper) {
        return null;
    }

    @Override // com.google.android.gms.internal.zzkh
    public zzkn getMobileAdsSettingsManagerWithClientJarVersion(IObjectWrapper iObjectWrapper, int i) {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        zzbs.zzbz();
        return zzax.zza(context, new zzaje(11020000, i, true, zzagz.zzO(context)));
    }
}
