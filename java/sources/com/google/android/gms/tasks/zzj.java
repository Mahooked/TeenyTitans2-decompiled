package com.google.android.gms.tasks;

/* JADX INFO: loaded from: classes.dex */
final class zzj implements Runnable {
    private /* synthetic */ Task zzbLT;
    private /* synthetic */ zzi zzbMb;

    zzj(zzi zziVar, Task task) {
        this.zzbMb = zziVar;
        this.zzbLT = task;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzbMb.mLock) {
            if (this.zzbMb.zzbMa != null) {
                this.zzbMb.zzbMa.onSuccess(this.zzbLT.getResult());
            }
        }
    }
}
