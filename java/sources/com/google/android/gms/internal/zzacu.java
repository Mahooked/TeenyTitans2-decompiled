package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzacu implements Runnable {
    private /* synthetic */ zzacs zzWv;
    private /* synthetic */ zzafg zzsW;

    zzacu(zzacs zzacsVar, zzafg zzafgVar) {
        this.zzWv = zzacsVar;
        this.zzsW = zzafgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzWv.zzb(new zzaff(this.zzsW, null, null, null, null, null, null, null));
    }
}
