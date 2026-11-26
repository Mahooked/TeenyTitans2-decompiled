package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzyg implements Runnable {
    private /* synthetic */ zzyf zzRi;
    private /* synthetic */ zzaff zztx;

    zzyg(zzyf zzyfVar, zzaff zzaffVar) {
        this.zzRi = zzyfVar;
        this.zztx = zzaffVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzRi.zzQP.zzb(this.zztx);
    }
}
