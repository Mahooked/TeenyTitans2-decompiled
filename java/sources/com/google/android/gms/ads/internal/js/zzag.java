package com.google.android.gms.ads.internal.js;

/* JADX INFO: loaded from: classes.dex */
final class zzag implements Runnable {
    private /* synthetic */ zza zzLy;
    private /* synthetic */ zzaf zzLz;

    zzag(zzaf zzafVar, zza zzaVar) {
        this.zzLz = zzafVar;
        this.zzLy = zzaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzLz.zzLx.zzLc.zzc(this.zzLy);
        this.zzLy.destroy();
    }
}
