package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzadx implements Runnable {
    private /* synthetic */ zzadw zzWT;
    private /* synthetic */ zzaff zztx;

    zzadx(zzadw zzadwVar, zzaff zzaffVar) {
        this.zzWT = zzadwVar;
        this.zztx = zzaffVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzWT.zzWS.zzb(this.zztx);
    }
}
