package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzxs implements Runnable {
    private /* synthetic */ zzxr zzQV;

    zzxs(zzxr zzxrVar) {
        this.zzQV = zzxrVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzQV.zzQU.get()) {
            zzafr.e("Timed out waiting for WebView to finish loading.");
            this.zzQV.cancel();
        }
    }
}
