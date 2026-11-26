package com.google.android.gms.internal;

import com.google.android.gms.ads.reward.RewardedVideoAdListener;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadi extends zzade {
    private final RewardedVideoAdListener zzcS;

    public zzadi(RewardedVideoAdListener rewardedVideoAdListener) {
        this.zzcS = rewardedVideoAdListener;
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdClosed() {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoAdClosed();
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdFailedToLoad(int i) {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoAdFailedToLoad(i);
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLeftApplication() {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoAdLeftApplication();
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdLoaded() {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoAdLoaded();
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoAdOpened() {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoAdOpened();
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void onRewardedVideoStarted() {
        if (this.zzcS != null) {
            this.zzcS.onRewardedVideoStarted();
        }
    }

    @Override // com.google.android.gms.internal.zzadd
    public final void zza(zzacv zzacvVar) {
        if (this.zzcS != null) {
            this.zzcS.onRewarded(new zzadg(zzacvVar));
        }
    }
}
