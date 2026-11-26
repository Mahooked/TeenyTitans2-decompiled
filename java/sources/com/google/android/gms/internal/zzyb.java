package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzyb implements Runnable {
    private /* synthetic */ zzaff zzQX;
    private /* synthetic */ zzya zzQY;

    zzyb(zzya zzyaVar, zzaff zzaffVar) {
        this.zzQY = zzyaVar;
        this.zzQX = zzaffVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzQY.zzQP.zzb(this.zzQX);
    }
}
