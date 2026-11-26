package com.google.android.gms.internal;

import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzagu implements Callable<Void> {
    private /* synthetic */ Runnable zzZm;

    zzagu(Runnable runnable) {
        this.zzZm = runnable;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ Void call() throws Exception {
        this.zzZm.run();
        return null;
    }
}
