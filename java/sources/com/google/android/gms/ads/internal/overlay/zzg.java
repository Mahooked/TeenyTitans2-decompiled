package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements Runnable {
    private /* synthetic */ zzd zzOA;
    private /* synthetic */ String zzOB;
    private /* synthetic */ String zzOC;

    zzg(zzd zzdVar, String str, String str2) {
        this.zzOA = zzdVar;
        this.zzOB = str;
        this.zzOC = str2;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzOA.zzOz != null) {
            this.zzOA.zzOz.zzj(this.zzOB, this.zzOC);
        }
    }
}
