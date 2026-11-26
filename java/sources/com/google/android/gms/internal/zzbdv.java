package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbdv implements Runnable {
    private /* synthetic */ String zzO;
    private /* synthetic */ zzbds zzaEK;
    private /* synthetic */ zzbdu zzaEL;

    zzbdv(zzbdu zzbduVar, zzbds zzbdsVar, String str) {
        this.zzaEL = zzbduVar;
        this.zzaEK = zzbdsVar;
        this.zzO = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzaEL.zzLe > 0) {
            this.zzaEK.onCreate(this.zzaEL.zzaEJ != null ? this.zzaEL.zzaEJ.getBundle(this.zzO) : null);
        }
        if (this.zzaEL.zzLe >= 2) {
            this.zzaEK.onStart();
        }
        if (this.zzaEL.zzLe >= 3) {
            this.zzaEK.onResume();
        }
        if (this.zzaEL.zzLe >= 4) {
            this.zzaEK.onStop();
        }
        if (this.zzaEL.zzLe >= 5) {
            this.zzaEK.onDestroy();
        }
    }
}
