package com.google.android.gms.internal;

import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;

/* JADX INFO: loaded from: classes.dex */
final class zzwg implements Runnable {
    private /* synthetic */ zzwd zzNr;
    private /* synthetic */ AdOverlayInfoParcel zzup;

    zzwg(zzwd zzwdVar, AdOverlayInfoParcel adOverlayInfoParcel) {
        this.zzNr = zzwdVar;
        this.zzup = adOverlayInfoParcel;
    }

    @Override // java.lang.Runnable
    public final void run() {
        com.google.android.gms.ads.internal.zzbs.zzbx();
        com.google.android.gms.ads.internal.overlay.zzu.zza(this.zzNr.zzNo, this.zzup, true);
    }
}
