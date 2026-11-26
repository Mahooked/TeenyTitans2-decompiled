package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzab implements Runnable {
    private /* synthetic */ zzaa zzPF;

    zzab(zzaa zzaaVar) {
        this.zzPF = zzaaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzPF.zza("surfaceCreated", new String[0]);
    }
}
