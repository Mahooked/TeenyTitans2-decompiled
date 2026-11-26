package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzih implements Runnable {
    private final int zzzI;
    private /* synthetic */ zzig zzzJ;

    public zzih(zzig zzigVar, int i) {
        this.zzzJ = zzigVar;
        this.zzzI = i;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzzJ) {
            if (this.zzzJ.zzzG == this.zzzI) {
                this.zzzJ.zzdg();
            }
        }
    }
}
