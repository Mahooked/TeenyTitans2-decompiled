package com.google.android.gms.ads.internal.js;

/* JADX INFO: loaded from: classes.dex */
final class zzp implements Runnable {
    private /* synthetic */ zzo zzLl;

    zzp(zzo zzoVar) {
        this.zzLl = zzoVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzLl.zzLk.zzLi.destroy();
    }
}
