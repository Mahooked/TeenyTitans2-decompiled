package com.google.android.gms.internal;

import com.google.android.gms.ads.doubleclick.PublisherAdView;

/* JADX INFO: loaded from: classes.dex */
final class zzqi implements Runnable {
    private /* synthetic */ PublisherAdView zzIQ;
    private /* synthetic */ zzjz zzIR;
    private /* synthetic */ zzqh zzIS;

    zzqi(zzqh zzqhVar, PublisherAdView publisherAdView, zzjz zzjzVar) {
        this.zzIS = zzqhVar;
        this.zzIQ = publisherAdView;
        this.zzIR = zzjzVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzIQ.zza(this.zzIR)) {
            this.zzIS.zzIP.onPublisherAdViewLoaded(this.zzIQ);
        } else {
            zzajc.zzaT("Could not bind ad manager");
        }
    }
}
