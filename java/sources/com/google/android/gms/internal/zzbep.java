package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbep implements Runnable {
    private /* synthetic */ String zzO;
    private /* synthetic */ zzbds zzaEK;
    private /* synthetic */ zzbeo zzaEZ;

    zzbep(zzbeo zzbeoVar, zzbds zzbdsVar, String str) {
        this.zzaEZ = zzbeoVar;
        this.zzaEK = zzbdsVar;
        this.zzO = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzaEZ.zzLe > 0) {
            this.zzaEK.onCreate(this.zzaEZ.zzaEJ != null ? this.zzaEZ.zzaEJ.getBundle(this.zzO) : null);
        }
        if (this.zzaEZ.zzLe >= 2) {
            this.zzaEK.onStart();
        }
        if (this.zzaEZ.zzLe >= 3) {
            this.zzaEK.onResume();
        }
        if (this.zzaEZ.zzLe >= 4) {
            this.zzaEK.onStop();
        }
        if (this.zzaEZ.zzLe >= 5) {
            this.zzaEK.onDestroy();
        }
    }
}
