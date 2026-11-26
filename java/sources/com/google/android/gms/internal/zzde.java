package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzde implements Runnable {
    private /* synthetic */ zzdb zzqW;

    zzde(zzdb zzdbVar) {
        this.zzqW = zzdbVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzmo.initialize(this.zzqW.zzqD);
    }
}
