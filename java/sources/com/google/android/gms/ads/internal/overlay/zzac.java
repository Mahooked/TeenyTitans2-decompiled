package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzac implements Runnable {
    private /* synthetic */ zzaa zzPF;

    zzac(zzaa zzaaVar) {
        this.zzPF = zzaaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzPF.zza("surfaceDestroyed", new String[0]);
    }
}
