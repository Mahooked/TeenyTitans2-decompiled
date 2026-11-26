package com.google.android.gms.internal;

import java.io.InputStream;
import java.util.concurrent.Callable;

/* JADX INFO: Add missing generic type declarations: [T] */
/* JADX INFO: loaded from: classes.dex */
final class zzail<T> implements Callable<T> {
    private /* synthetic */ InputStream zzaam;
    private /* synthetic */ zzaij zzaan;

    zzail(zzaij zzaijVar, InputStream inputStream) {
        this.zzaan = zzaijVar;
        this.zzaam = inputStream;
    }

    @Override // java.util.concurrent.Callable
    public final T call() {
        return (T) this.zzaan.zzaaj.zzh(this.zzaam);
    }
}
