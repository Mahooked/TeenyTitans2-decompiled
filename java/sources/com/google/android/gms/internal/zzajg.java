package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzajg<T> implements zzajm<T> {
    private T mValue;
    private boolean zzMP;
    private Throwable zzaaS;
    private boolean zzaaT;
    private final Object mLock = new Object();
    private final zzajn zzaaU = new zzajn();

    private final boolean zzim() {
        return this.zzaaS != null || this.zzaaT;
    }

    @Override // java.util.concurrent.Future
    public boolean cancel(boolean z) {
        boolean z2 = false;
        if (z) {
            synchronized (this.mLock) {
                if (!zzim()) {
                    this.zzMP = true;
                    this.zzaaT = true;
                    this.mLock.notifyAll();
                    this.zzaaU.zzin();
                    z2 = true;
                }
            }
        }
        return z2;
    }

    @Override // java.util.concurrent.Future
    public T get() throws ExecutionException, CancellationException, InterruptedException {
        T t;
        synchronized (this.mLock) {
            if (!zzim()) {
                try {
                    this.mLock.wait();
                } catch (InterruptedException e) {
                    throw e;
                }
            }
            if (this.zzaaS != null) {
                throw new ExecutionException(this.zzaaS);
            }
            if (this.zzMP) {
                throw new CancellationException("CallbackFuture was cancelled.");
            }
            t = this.mValue;
        }
        return t;
    }

    @Override // java.util.concurrent.Future
    public T get(long j, TimeUnit timeUnit) throws ExecutionException, CancellationException, InterruptedException, TimeoutException {
        T t;
        synchronized (this.mLock) {
            if (!zzim()) {
                try {
                    long millis = timeUnit.toMillis(j);
                    if (millis != 0) {
                        this.mLock.wait(millis);
                    }
                } catch (InterruptedException e) {
                    throw e;
                }
            }
            if (this.zzaaS != null) {
                throw new ExecutionException(this.zzaaS);
            }
            if (!this.zzaaT) {
                throw new TimeoutException("CallbackFuture timed out.");
            }
            if (this.zzMP) {
                throw new CancellationException("CallbackFuture was cancelled.");
            }
            t = this.mValue;
        }
        return t;
    }

    @Override // java.util.concurrent.Future
    public boolean isCancelled() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzMP;
        }
        return z;
    }

    @Override // java.util.concurrent.Future
    public boolean isDone() {
        boolean zZzim;
        synchronized (this.mLock) {
            zZzim = zzim();
        }
        return zZzim;
    }

    public final void zzb(Throwable th) {
        synchronized (this.mLock) {
            if (this.zzMP) {
                return;
            }
            if (zzim()) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(new IllegalStateException("Provided CallbackFuture with multiple values."), "CallbackFuture.provideException");
                return;
            }
            this.zzaaS = th;
            this.mLock.notifyAll();
            this.zzaaU.zzin();
        }
    }

    @Override // com.google.android.gms.internal.zzajm
    public final void zzc(Runnable runnable) {
        this.zzaaU.zzc(runnable);
    }

    @Override // com.google.android.gms.internal.zzajm
    public final void zzd(Runnable runnable) {
        this.zzaaU.zzd(runnable);
    }

    public final void zzg(@Nullable T t) {
        synchronized (this.mLock) {
            if (this.zzMP) {
                return;
            }
            if (zzim()) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(new IllegalStateException("Provided CallbackFuture with multiple values."), "CallbackFuture.provideValue");
                return;
            }
            this.zzaaT = true;
            this.mValue = t;
            this.mLock.notifyAll();
            this.zzaaU.zzin();
        }
    }
}
