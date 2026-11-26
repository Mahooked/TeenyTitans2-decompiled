package com.google.android.gms.ads.internal.js;

/* JADX INFO: loaded from: classes.dex */
final class zzt implements Runnable {
    private /* synthetic */ zzs zzLn;

    zzt(zzs zzsVar) {
        this.zzLn = zzsVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzLn.zzLi.destroy();
    }
}
