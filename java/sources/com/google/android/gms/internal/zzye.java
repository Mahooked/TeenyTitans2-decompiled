package com.google.android.gms.internal;

import java.util.concurrent.CountDownLatch;

/* JADX INFO: loaded from: classes.dex */
final class zzye implements Runnable {
    private /* synthetic */ zzyd zzRf;
    private /* synthetic */ CountDownLatch zzsT;

    zzye(zzyd zzydVar, CountDownLatch countDownLatch) {
        this.zzRf = zzydVar;
        this.zzsT = countDownLatch;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzRf.zzQT) {
            this.zzRf.zzRe = com.google.android.gms.ads.internal.zzar.zza(this.zzRf.zzJH, this.zzRf.zzRd, this.zzsT);
        }
    }
}
