package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.customtabs.CustomTabsIntent;
import android.text.TextUtils;
import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;
import com.google.android.gms.ads.mediation.MediationAdRequest;
import com.google.android.gms.ads.mediation.MediationInterstitialAdapter;
import com.google.android.gms.ads.mediation.MediationInterstitialListener;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwd implements MediationInterstitialAdapter {
    private Uri mUri;
    private Activity zzNo;
    private zznl zzNp;
    private MediationInterstitialListener zzNq;

    @Override // com.google.android.gms.ads.mediation.MediationAdapter
    public void onDestroy() {
        zzajc.zzaC("Destroying AdMobCustomTabsAdapter adapter.");
        try {
            this.zzNp.zzc(this.zzNo);
        } catch (Exception e) {
            zzajc.zzb("Exception while unbinding from CustomTabsService.", e);
        }
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdapter
    public void onPause() {
        zzajc.zzaC("Pausing AdMobCustomTabsAdapter adapter.");
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdapter
    public void onResume() {
        zzajc.zzaC("Resuming AdMobCustomTabsAdapter adapter.");
    }

    @Override // com.google.android.gms.ads.mediation.MediationInterstitialAdapter
    public final void requestInterstitialAd(Context context, MediationInterstitialListener mediationInterstitialListener, Bundle bundle, MediationAdRequest mediationAdRequest, Bundle bundle2) {
        this.zzNq = mediationInterstitialListener;
        if (this.zzNq == null) {
            zzajc.zzaT("Listener not set for mediation. Returning.");
            return;
        }
        if (!(context instanceof Activity)) {
            zzajc.zzaT("AdMobCustomTabs can only work with Activity context. Bailing out.");
            this.zzNq.onAdFailedToLoad(this, 0);
            return;
        }
        if (!zznl.zzi(context)) {
            zzajc.zzaT("Default browser does not support custom tabs. Bailing out.");
            this.zzNq.onAdFailedToLoad(this, 0);
            return;
        }
        String string = bundle.getString("tab_url");
        if (TextUtils.isEmpty(string)) {
            zzajc.zzaT("The tab_url retrieved from mediation metadata is empty. Bailing out.");
            this.zzNq.onAdFailedToLoad(this, 0);
            return;
        }
        this.zzNo = (Activity) context;
        this.mUri = Uri.parse(string);
        this.zzNp = new zznl();
        this.zzNp.zza(new zzwe(this));
        this.zzNp.zzd(this.zzNo);
        this.zzNq.onAdLoaded(this);
    }

    @Override // com.google.android.gms.ads.mediation.MediationInterstitialAdapter
    public final void showInterstitial() {
        CustomTabsIntent customTabsIntentBuild = new CustomTabsIntent.Builder(this.zzNp.zzdY()).build();
        customTabsIntentBuild.intent.setData(this.mUri);
        zzagz.zzZr.post(new zzwg(this, new AdOverlayInfoParcel(new com.google.android.gms.ads.internal.overlay.zzc(customTabsIntentBuild.intent), null, new zzwf(this), null, new zzaje(0, 0, false))));
        com.google.android.gms.ads.internal.zzbs.zzbD().zzz(false);
    }
}
