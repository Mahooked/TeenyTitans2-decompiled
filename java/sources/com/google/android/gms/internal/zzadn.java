package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzadn implements Runnable {
    private /* synthetic */ zzut zzWF;
    private /* synthetic */ zzadm zzWG;
    private /* synthetic */ zzir zztY;

    zzadn(zzadm zzadmVar, zzir zzirVar, zzut zzutVar) {
        this.zzWG = zzadmVar;
        this.zztY = zzirVar;
        this.zzWF = zzutVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzWG.zza(this.zztY, this.zzWF);
    }
}
