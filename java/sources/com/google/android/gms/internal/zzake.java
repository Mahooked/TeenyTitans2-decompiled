package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzake implements Runnable {
    private /* synthetic */ zzakb zzabJ;

    zzake(zzakb zzakbVar) {
        this.zzabJ = zzakbVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzabJ.zzJH.zziJ();
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = this.zzabJ.zzJH.zziu();
        if (zzmVarZziu != null) {
            zzmVarZziu.zzfL();
        }
        if (this.zzabJ.zzabt != null) {
            this.zzabJ.zzabt.zzaT();
            zzakb.zza(this.zzabJ, (zzakh) null);
        }
    }
}
