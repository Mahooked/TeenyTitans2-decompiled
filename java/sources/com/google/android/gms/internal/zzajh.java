package com.google.android.gms.internal;

import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzajh<T> implements zzajm<T> {
    private final T mValue;
    private final zzajn zzaaU = new zzajn();

    public zzajh(T t) {
        this.mValue = t;
        this.zzaaU.zzin();
    }

    @Override // java.util.concurrent.Future
    public final boolean cancel(boolean z) {
        return false;
    }

    @Override // java.util.concurrent.Future
    public final T get() {
        return this.mValue;
    }

    @Override // java.util.concurrent.Future
    public final T get(long j, TimeUnit timeUnit) {
        return this.mValue;
    }

    @Override // java.util.concurrent.Future
    public final boolean isCancelled() {
        return false;
    }

    @Override // java.util.concurrent.Future
    public final boolean isDone() {
        return true;
    }

    @Override // com.google.android.gms.internal.zzajm
    public final void zzc(Runnable runnable) {
        this.zzaaU.zzc(runnable);
    }

    @Override // com.google.android.gms.internal.zzajm
    public final void zzd(Runnable runnable) {
        this.zzaaU.zzd(runnable);
    }
}
