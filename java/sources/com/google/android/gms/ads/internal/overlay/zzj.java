package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzj implements Runnable {
    private /* synthetic */ zzd zzOA;

    zzj(zzd zzdVar) {
        this.zzOA = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzOA.zzOz != null) {
            this.zzOA.zzOz.onPaused();
            this.zzOA.zzOz.zzfW();
        }
    }
}
