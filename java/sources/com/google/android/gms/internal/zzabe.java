package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzabe implements Runnable {
    private /* synthetic */ zzaaz zzUc;

    zzabe(zzaaz zzaazVar) {
        this.zzUc = zzaazVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzUc.zzUb != null) {
            this.zzUc.zzUb.release();
            this.zzUc.zzUb = null;
        }
    }
}
