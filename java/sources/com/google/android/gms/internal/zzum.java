package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzum implements Runnable {
    private /* synthetic */ zzuk zzMU;
    private /* synthetic */ zzajm zzMV;

    zzum(zzuk zzukVar, zzajm zzajmVar) {
        this.zzMU = zzukVar;
        this.zzMV = zzajmVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        for (zzajm zzajmVar : this.zzMU.zzMQ.keySet()) {
            if (zzajmVar != this.zzMV) {
                ((zzue) this.zzMU.zzMQ.get(zzajmVar)).cancel();
            }
        }
    }
}
