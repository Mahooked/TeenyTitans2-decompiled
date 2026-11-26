package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbbl implements Runnable {
    private /* synthetic */ zzbbk zzaCx;

    zzbbl(zzbbk zzbbkVar) {
        this.zzaCx = zzbbkVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzaCx.zzaCv.lock();
        try {
            this.zzaCx.zzpF();
        } finally {
            this.zzaCx.zzaCv.unlock();
        }
    }
}
