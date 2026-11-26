package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzafq implements Runnable {
    private /* synthetic */ zzafp zzYX;

    zzafq(zzafp zzafpVar) {
        this.zzYX = zzafpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzYX.zzYV = Thread.currentThread();
        this.zzYX.zzbd();
    }
}
