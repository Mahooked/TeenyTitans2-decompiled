package com.google.android.gms.ads.internal.overlay;

import android.os.Looper;

/* JADX INFO: loaded from: classes.dex */
final class zzan implements Runnable {
    zzan(zzam zzamVar) {
    }

    @Override // java.lang.Runnable
    public final void run() {
        Looper.myLooper().quit();
    }
}
