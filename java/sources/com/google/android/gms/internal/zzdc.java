package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzdc implements Runnable {
    private /* synthetic */ zzdb zzqW;

    zzdc(zzdb zzdbVar) {
        this.zzqW = zzdbVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzqW.zzM();
    }
}
