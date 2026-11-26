package com.google.android.gms.internal;

import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
final class zzagx implements Runnable {
    private /* synthetic */ zzajg zzZn;
    private /* synthetic */ Future zzZp;

    zzagx(zzajg zzajgVar, Future future) {
        this.zzZn = zzajgVar;
        this.zzZp = future;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzZn.isCancelled()) {
            this.zzZp.cancel(true);
        }
    }
}
