package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;

/* JADX INFO: loaded from: classes.dex */
abstract class zzbcn implements Runnable {
    private /* synthetic */ zzbcd zzaDp;

    private zzbcn(zzbcd zzbcdVar) {
        this.zzaDp = zzbcdVar;
    }

    /* synthetic */ zzbcn(zzbcd zzbcdVar, zzbce zzbceVar) {
        this(zzbcdVar);
    }

    @Override // java.lang.Runnable
    @WorkerThread
    public void run() {
        this.zzaDp.zzaCv.lock();
        try {
            if (Thread.interrupted()) {
                return;
            }
            zzpV();
            return;
        } catch (RuntimeException e) {
            this.zzaDp.zzaCZ.zza(e);
            return;
        } finally {
            this.zzaDp.zzaCv.unlock();
        }
        this.zzaDp.zzaCv.unlock();
    }

    @WorkerThread
    protected abstract void zzpV();
}
