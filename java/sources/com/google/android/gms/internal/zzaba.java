package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzaba implements Runnable {
    private /* synthetic */ zzaaz zzUc;
    private /* synthetic */ zzafg zzsW;

    zzaba(zzaaz zzaazVar, zzafg zzafgVar) {
        this.zzUc = zzaazVar;
        this.zzsW = zzafgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzUc.zzSm.zza(this.zzsW);
        if (this.zzUc.zzUb != null) {
            this.zzUc.zzUb.release();
            this.zzUc.zzUb = null;
        }
    }
}
