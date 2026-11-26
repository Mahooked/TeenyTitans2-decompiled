package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbek implements Runnable {
    private /* synthetic */ zzctx zzaDx;
    private /* synthetic */ zzbej zzaEY;

    zzbek(zzbej zzbejVar, zzctx zzctxVar) {
        this.zzaEY = zzbejVar;
        this.zzaDx = zzctxVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzaEY.zzc(this.zzaDx);
    }
}
