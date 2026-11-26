package com.google.android.gms.internal;

import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
final class zzabs implements Runnable {
    private /* synthetic */ Future zzUJ;

    zzabs(zzabm zzabmVar, Future future) {
        this.zzUJ = future;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzUJ.isDone()) {
            return;
        }
        this.zzUJ.cancel(true);
    }
}
