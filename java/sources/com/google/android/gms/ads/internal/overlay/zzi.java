package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzi implements Runnable {
    private /* synthetic */ zzd zzOA;
    private /* synthetic */ int zzOD;
    private /* synthetic */ int zzOE;

    zzi(zzd zzdVar, int i, int i2) {
        this.zzOA = zzdVar;
        this.zzOD = i;
        this.zzOE = i2;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzOA.zzOz != null) {
            this.zzOA.zzOz.zzd(this.zzOD, this.zzOE);
        }
    }
}
