package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzyq implements Runnable {
    private /* synthetic */ zzyn zzRF;
    private /* synthetic */ zzajg zzRH;
    private /* synthetic */ String zzRI;

    zzyq(zzyn zzynVar, zzajg zzajgVar, String str) {
        this.zzRF = zzynVar;
        this.zzRH = zzajgVar;
        this.zzRI = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzRH.zzg(this.zzRF.zzRw.zzbl().get(this.zzRI));
    }
}
