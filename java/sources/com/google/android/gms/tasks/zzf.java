package com.google.android.gms.tasks;

/* JADX INFO: loaded from: classes.dex */
final class zzf implements Runnable {
    private /* synthetic */ Task zzbLT;
    private /* synthetic */ zze zzbLX;

    zzf(zze zzeVar, Task task) {
        this.zzbLX = zzeVar;
        this.zzbLT = task;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzbLX.mLock) {
            if (this.zzbLX.zzbLW != null) {
                this.zzbLX.zzbLW.onComplete(this.zzbLT);
            }
        }
    }
}
