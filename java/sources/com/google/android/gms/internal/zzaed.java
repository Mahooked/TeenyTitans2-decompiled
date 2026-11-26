package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.ads.reward.RewardItem;
import com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdAdapter;
import com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaed implements MediationRewardedVideoAdListener {
    private final zzaea zzWV;

    public zzaed(zzaea zzaeaVar) {
        this.zzWV = zzaeaVar;
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdClicked(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onAdClicked must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdClicked.");
        try {
            this.zzWV.zzv(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdClicked.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdClosed(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onAdClosed must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdClosed.");
        try {
            this.zzWV.zzu(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdClosed.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdFailedToLoad(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter, int i) {
        zzbo.zzcz("onAdFailedToLoad must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdFailedToLoad.");
        try {
            this.zzWV.zzd(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter), i);
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdFailedToLoad.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdLeftApplication(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onAdLeftApplication must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdLeftApplication.");
        try {
            this.zzWV.zzw(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdLeftApplication.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdLoaded(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onAdLoaded must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdLoaded.");
        try {
            this.zzWV.zzr(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdLoaded.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onAdOpened(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onAdOpened must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onAdOpened.");
        try {
            this.zzWV.zzs(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdOpened.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onInitializationFailed(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter, int i) {
        zzbo.zzcz("onInitializationFailed must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onInitializationFailed.");
        try {
            this.zzWV.zzc(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter), i);
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onInitializationFailed.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onInitializationSucceeded(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onInitializationSucceeded must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onInitializationSucceeded.");
        try {
            this.zzWV.zzq(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onInitializationSucceeded.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onRewarded(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter, RewardItem rewardItem) {
        zzbo.zzcz("onRewarded must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onRewarded.");
        try {
            if (rewardItem != null) {
                this.zzWV.zza(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter), new zzaee(rewardItem));
            } else {
                this.zzWV.zza(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter), new zzaee("", 1));
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onRewarded.", e);
        }
    }

    @Override // com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdListener
    public final void onVideoStarted(MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter) {
        zzbo.zzcz("onVideoStarted must be called on the main UI thread.");
        zzajc.zzaC("Adapter called onVideoStarted.");
        try {
            this.zzWV.zzt(com.google.android.gms.dynamic.zzn.zzw(mediationRewardedVideoAdAdapter));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onVideoStarted.", e);
        }
    }
}
