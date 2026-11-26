package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzwf implements com.google.android.gms.ads.internal.overlay.zzw {
    private /* synthetic */ zzwd zzNr;

    zzwf(zzwd zzwdVar) {
        this.zzNr = zzwdVar;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onPause() {
        zzajc.zzaC("AdMobCustomTabsAdapter overlay is paused.");
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onResume() {
        zzajc.zzaC("AdMobCustomTabsAdapter overlay is resumed.");
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaA() {
        zzajc.zzaC("AdMobCustomTabsAdapter overlay is closed.");
        this.zzNr.zzNq.onAdClosed(this.zzNr);
        try {
            this.zzNr.zzNp.zzc(this.zzNr.zzNo);
        } catch (Exception e) {
            zzajc.zzb("Exception while unbinding from CustomTabsService.", e);
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaB() {
        zzajc.zzaC("Opening AdMobCustomTabsAdapter overlay.");
        this.zzNr.zzNq.onAdOpened(this.zzNr);
    }
}
