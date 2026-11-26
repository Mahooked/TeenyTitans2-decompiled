package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zze implements Runnable {
    private /* synthetic */ zzd zzOA;

    zze(zzd zzdVar) {
        this.zzOA = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzOA.zzOz != null) {
            this.zzOA.zzOz.zzfT();
        }
    }
}
