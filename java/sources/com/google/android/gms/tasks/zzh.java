package com.google.android.gms.tasks;

/* JADX INFO: loaded from: classes.dex */
final class zzh implements Runnable {
    private /* synthetic */ Task zzbLT;
    private /* synthetic */ zzg zzbLZ;

    zzh(zzg zzgVar, Task task) {
        this.zzbLZ = zzgVar;
        this.zzbLT = task;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzbLZ.mLock) {
            if (this.zzbLZ.zzbLY != null) {
                this.zzbLZ.zzbLY.onFailure(this.zzbLT.getException());
            }
        }
    }
}
