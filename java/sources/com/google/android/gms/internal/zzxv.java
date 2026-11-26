package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzxv implements Runnable {
    private /* synthetic */ zzxt zzQW;
    private /* synthetic */ zzaff zztx;

    zzxv(zzxt zzxtVar, zzaff zzaffVar) {
        this.zzQW = zzxtVar;
        this.zztx = zzaffVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzQW.mLock) {
            zzxt zzxtVar = this.zzQW;
            zzxtVar.zzQP.zzb(this.zztx);
        }
    }
}
