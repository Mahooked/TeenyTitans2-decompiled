package com.google.android.gms.ads.internal;

import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;

/* JADX INFO: loaded from: classes.dex */
final class zzao implements Runnable {
    private /* synthetic */ AdOverlayInfoParcel zzup;
    private /* synthetic */ zzan zzuq;

    zzao(zzan zzanVar, AdOverlayInfoParcel adOverlayInfoParcel) {
        this.zzuq = zzanVar;
        this.zzup = adOverlayInfoParcel;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzbs.zzbx();
        com.google.android.gms.ads.internal.overlay.zzu.zza(this.zzuq.zzun.zzsP.zzqD, this.zzup, true);
    }
}
