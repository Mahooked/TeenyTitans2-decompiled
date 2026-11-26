package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.ads.AdRequest;
import com.google.ads.mediation.MediationBannerAdapter;
import com.google.ads.mediation.MediationBannerListener;
import com.google.ads.mediation.MediationInterstitialAdapter;
import com.google.ads.mediation.MediationInterstitialListener;
import com.google.ads.mediation.MediationServerParameters;
import com.google.ads.mediation.NetworkExtras;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvp<NETWORK_EXTRAS extends NetworkExtras, SERVER_PARAMETERS extends MediationServerParameters> implements MediationBannerListener, MediationInterstitialListener {
    private final zzuw zzNc;

    public zzvp(zzuw zzuwVar) {
        this.zzNc = zzuwVar;
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onClick(MediationBannerAdapter<?, ?> mediationBannerAdapter) {
        zzajc.zzaC("Adapter called onClick.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onClick must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvq(this));
        } else {
            try {
                this.zzNc.onAdClicked();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdClicked.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onDismissScreen(MediationBannerAdapter<?, ?> mediationBannerAdapter) {
        zzajc.zzaC("Adapter called onDismissScreen.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onDismissScreen must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvt(this));
        } else {
            try {
                this.zzNc.onAdClosed();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdClosed.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationInterstitialListener
    public final void onDismissScreen(MediationInterstitialAdapter<?, ?> mediationInterstitialAdapter) {
        zzajc.zzaC("Adapter called onDismissScreen.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onDismissScreen must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvy(this));
        } else {
            try {
                this.zzNc.onAdClosed();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdClosed.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onFailedToReceiveAd(MediationBannerAdapter<?, ?> mediationBannerAdapter, AdRequest.ErrorCode errorCode) {
        String strValueOf = String.valueOf(errorCode);
        zzajc.zzaC(new StringBuilder(String.valueOf(strValueOf).length() + 47).append("Adapter called onFailedToReceiveAd with error. ").append(strValueOf).toString());
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onFailedToReceiveAd must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvu(this, errorCode));
        } else {
            try {
                this.zzNc.onAdFailedToLoad(zzwb.zza(errorCode));
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdFailedToLoad.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationInterstitialListener
    public final void onFailedToReceiveAd(MediationInterstitialAdapter<?, ?> mediationInterstitialAdapter, AdRequest.ErrorCode errorCode) {
        String strValueOf = String.valueOf(errorCode);
        zzajc.zzaC(new StringBuilder(String.valueOf(strValueOf).length() + 47).append("Adapter called onFailedToReceiveAd with error ").append(strValueOf).append(".").toString());
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onFailedToReceiveAd must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvz(this, errorCode));
        } else {
            try {
                this.zzNc.onAdFailedToLoad(zzwb.zza(errorCode));
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdFailedToLoad.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onLeaveApplication(MediationBannerAdapter<?, ?> mediationBannerAdapter) {
        zzajc.zzaC("Adapter called onLeaveApplication.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onLeaveApplication must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvv(this));
        } else {
            try {
                this.zzNc.onAdLeftApplication();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdLeftApplication.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationInterstitialListener
    public final void onLeaveApplication(MediationInterstitialAdapter<?, ?> mediationInterstitialAdapter) {
        zzajc.zzaC("Adapter called onLeaveApplication.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onLeaveApplication must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzwa(this));
        } else {
            try {
                this.zzNc.onAdLeftApplication();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdLeftApplication.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onPresentScreen(MediationBannerAdapter<?, ?> mediationBannerAdapter) {
        zzajc.zzaC("Adapter called onPresentScreen.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onPresentScreen must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvw(this));
        } else {
            try {
                this.zzNc.onAdOpened();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdOpened.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationInterstitialListener
    public final void onPresentScreen(MediationInterstitialAdapter<?, ?> mediationInterstitialAdapter) {
        zzajc.zzaC("Adapter called onPresentScreen.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onPresentScreen must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvr(this));
        } else {
            try {
                this.zzNc.onAdOpened();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdOpened.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationBannerListener
    public final void onReceivedAd(MediationBannerAdapter<?, ?> mediationBannerAdapter) {
        zzajc.zzaC("Adapter called onReceivedAd.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onReceivedAd must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvx(this));
        } else {
            try {
                this.zzNc.onAdLoaded();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdLoaded.", e);
            }
        }
    }

    @Override // com.google.ads.mediation.MediationInterstitialListener
    public final void onReceivedAd(MediationInterstitialAdapter<?, ?> mediationInterstitialAdapter) {
        zzajc.zzaC("Adapter called onReceivedAd.");
        zzji.zzds();
        if (!zzaiy.zzil()) {
            zzajc.zzaT("onReceivedAd must be called on the main UI thread.");
            zzaiy.zzaaH.post(new zzvs(this));
        } else {
            try {
                this.zzNc.onAdLoaded();
            } catch (RemoteException e) {
                zzajc.zzc("Could not call onAdLoaded.", e);
            }
        }
    }
}
