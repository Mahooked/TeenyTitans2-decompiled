package com.google.android.gms.ads.internal.overlay;

/* JADX INFO: loaded from: classes.dex */
final class zzal implements Runnable {
    private /* synthetic */ zzx zzPL;

    zzal(zzak zzakVar, zzx zzxVar) {
        this.zzPL = zzxVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzPL.zzfX();
    }
}
