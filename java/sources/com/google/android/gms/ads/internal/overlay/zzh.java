package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzh implements Runnable {
    private /* synthetic */ zzd zzOA;

    zzh(zzd zzdVar) {
        this.zzOA = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzOA.zzOz != null) {
            this.zzOA.zzOz.zzfS();
        }
    }
}
