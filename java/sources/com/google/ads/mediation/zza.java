package com.google.ads.mediation;

import com.google.android.gms.ads.InterstitialAd;
import com.google.android.gms.ads.reward.RewardItem;
import com.google.android.gms.ads.reward.RewardedVideoAdListener;

/* JADX INFO: loaded from: classes.dex */
final class zza implements RewardedVideoAdListener {
    private /* synthetic */ AbstractAdViewAdapter zzcT;

    zza(AbstractAdViewAdapter abstractAdViewAdapter) {
        this.zzcT = abstractAdViewAdapter;
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewarded(RewardItem rewardItem) {
        this.zzcT.zzcR.onRewarded(this.zzcT, rewardItem);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoAdClosed() {
        this.zzcT.zzcR.onAdClosed(this.zzcT);
        AbstractAdViewAdapter.zza(this.zzcT, (InterstitialAd) null);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoAdFailedToLoad(int i) {
        this.zzcT.zzcR.onAdFailedToLoad(this.zzcT, i);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoAdLeftApplication() {
        this.zzcT.zzcR.onAdLeftApplication(this.zzcT);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoAdLoaded() {
        this.zzcT.zzcR.onAdLoaded(this.zzcT);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoAdOpened() {
        this.zzcT.zzcR.onAdOpened(this.zzcT);
    }

    @Override // com.google.android.gms.ads.reward.RewardedVideoAdListener
    public final void onRewardedVideoStarted() {
        this.zzcT.zzcR.onVideoStarted(this.zzcT);
    }
}
