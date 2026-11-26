package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.ads.reward.RewardedVideoAd;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzlf {
    private static zzlf zzBn;
    private static final Object zzuF = new Object();
    private zzkn zzBo;
    private RewardedVideoAd zzBp;

    private zzlf() {
    }

    public static zzlf zzdD() {
        zzlf zzlfVar;
        synchronized (zzuF) {
            if (zzBn == null) {
                zzBn = new zzlf();
            }
            zzlfVar = zzBn;
        }
        return zzlfVar;
    }

    public final RewardedVideoAd getRewardedVideoAdInstance(Context context) {
        RewardedVideoAd rewardedVideoAd;
        synchronized (zzuF) {
            if (this.zzBp != null) {
                rewardedVideoAd = this.zzBp;
            } else {
                this.zzBp = new zzadl(context, (zzacy) zziz.zza(context, false, (zziz.zza) new zzjg(zzji.zzdt(), context, new zzup())));
                rewardedVideoAd = this.zzBp;
            }
        }
        return rewardedVideoAd;
    }

    public final void openDebugMenu(Context context, String str) {
        zzbo.zza(this.zzBo != null, "MobileAds.initialize() must be called prior to opening debug menu.");
        try {
            this.zzBo.zzb(com.google.android.gms.dynamic.zzn.zzw(context), str);
        } catch (RemoteException e) {
            zzajc.zzb("Unable to open debug menu.", e);
        }
    }

    public final void setAppMuted(boolean z) {
        zzbo.zza(this.zzBo != null, "MobileAds.initialize() must be called prior to setting the app volume.");
        try {
            this.zzBo.setAppMuted(z);
        } catch (RemoteException e) {
            zzajc.zzb("Unable to set app mute state.", e);
        }
    }

    public final void setAppVolume(float f) {
        zzbo.zzb(0.0f <= f && f <= 1.0f, "The app volume must be a value between 0 and 1 inclusive.");
        zzbo.zza(this.zzBo != null, "MobileAds.initialize() must be called prior to setting the app volume.");
        try {
            this.zzBo.setAppVolume(f);
        } catch (RemoteException e) {
            zzajc.zzb("Unable to set app volume.", e);
        }
    }

    public final void zza(Context context, String str, zzlh zzlhVar) {
        synchronized (zzuF) {
            if (this.zzBo != null) {
                return;
            }
            if (context == null) {
                throw new IllegalArgumentException("Context cannot be null.");
            }
            try {
                this.zzBo = (zzkn) zziz.zza(context, false, (zziz.zza) new zzje(zzji.zzdt(), context));
                this.zzBo.initialize();
                if (str != null) {
                    this.zzBo.zzc(str, com.google.android.gms.dynamic.zzn.zzw(new zzlg(this, context)));
                }
            } catch (RemoteException e) {
                zzajc.zzc("MobileAdsSettingManager initialization failed", e);
            }
        }
    }
}
