package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzair {
    private long zzaau;
    private long zzaav = Long.MIN_VALUE;
    private Object mLock = new Object();

    public zzair(long j) {
        this.zzaau = j;
    }

    public final boolean tryAcquire() {
        boolean z;
        synchronized (this.mLock) {
            long jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
            if (this.zzaav + this.zzaau > jElapsedRealtime) {
                z = false;
            } else {
                this.zzaav = jElapsedRealtime;
                z = true;
            }
        }
        return z;
    }
}
