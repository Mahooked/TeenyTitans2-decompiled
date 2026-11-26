package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbo;
import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;
import java.util.concurrent.Callable;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
public final class Tasks {

    static final class zza implements zzb {
        private final CountDownLatch zztJ;

        private zza() {
            this.zztJ = new CountDownLatch(1);
        }

        /* synthetic */ zza(zzo zzoVar) {
            this();
        }

        public final void await() throws InterruptedException {
            this.zztJ.await();
        }

        public final boolean await(long j, TimeUnit timeUnit) throws InterruptedException {
            return this.zztJ.await(j, timeUnit);
        }

        @Override // com.google.android.gms.tasks.OnFailureListener
        public final void onFailure(@NonNull Exception exc) {
            this.zztJ.countDown();
        }

        @Override // com.google.android.gms.tasks.OnSuccessListener
        public final void onSuccess(Object obj) {
            this.zztJ.countDown();
        }
    }

    interface zzb extends OnFailureListener, OnSuccessListener<Object> {
    }

    static final class zzc implements zzb {
        private final Object mLock = new Object();
        private final zzn<Void> zzbMe;
        private Exception zzbMj;
        private final int zzbMl;
        private int zzbMm;
        private int zzbMn;

        public zzc(int i, zzn<Void> zznVar) {
            this.zzbMl = i;
            this.zzbMe = zznVar;
        }

        private final void zzDJ() {
            if (this.zzbMm + this.zzbMn == this.zzbMl) {
                if (this.zzbMj == null) {
                    this.zzbMe.setResult(null);
                    return;
                }
                zzn<Void> zznVar = this.zzbMe;
                int i = this.zzbMn;
                zznVar.setException(new ExecutionException(new StringBuilder(54).append(i).append(" out of ").append(this.zzbMl).append(" underlying tasks failed").toString(), this.zzbMj));
            }
        }

        @Override // com.google.android.gms.tasks.OnFailureListener
        public final void onFailure(@NonNull Exception exc) {
            synchronized (this.mLock) {
                this.zzbMn++;
                this.zzbMj = exc;
                zzDJ();
            }
        }

        @Override // com.google.android.gms.tasks.OnSuccessListener
        public final void onSuccess(Object obj) {
            synchronized (this.mLock) {
                this.zzbMm++;
                zzDJ();
            }
        }
    }

    private Tasks() {
    }

    public static <TResult> TResult await(@NonNull Task<TResult> task) throws ExecutionException, InterruptedException {
        zzbo.zzcG("Must not be called on the main application thread");
        zzbo.zzb(task, "Task must not be null");
        if (task.isComplete()) {
            return (TResult) zzb(task);
        }
        zza zzaVar = new zza(null);
        zza(task, zzaVar);
        zzaVar.await();
        return (TResult) zzb(task);
    }

    public static <TResult> TResult await(@NonNull Task<TResult> task, long j, @NonNull TimeUnit timeUnit) throws ExecutionException, InterruptedException, TimeoutException {
        zzbo.zzcG("Must not be called on the main application thread");
        zzbo.zzb(task, "Task must not be null");
        zzbo.zzb(timeUnit, "TimeUnit must not be null");
        if (task.isComplete()) {
            return (TResult) zzb(task);
        }
        zza zzaVar = new zza(null);
        zza(task, zzaVar);
        if (zzaVar.await(j, timeUnit)) {
            return (TResult) zzb(task);
        }
        throw new TimeoutException("Timed out waiting for Task");
    }

    public static <TResult> Task<TResult> call(@NonNull Callable<TResult> callable) {
        return call(TaskExecutors.MAIN_THREAD, callable);
    }

    public static <TResult> Task<TResult> call(@NonNull Executor executor, @NonNull Callable<TResult> callable) {
        zzbo.zzb(executor, "Executor must not be null");
        zzbo.zzb(callable, "Callback must not be null");
        zzn zznVar = new zzn();
        executor.execute(new zzo(zznVar, callable));
        return zznVar;
    }

    public static <TResult> Task<TResult> forException(@NonNull Exception exc) {
        zzn zznVar = new zzn();
        zznVar.setException(exc);
        return zznVar;
    }

    public static <TResult> Task<TResult> forResult(TResult tresult) {
        zzn zznVar = new zzn();
        zznVar.setResult(tresult);
        return zznVar;
    }

    public static Task<Void> whenAll(Collection<? extends Task<?>> collection) {
        if (collection.isEmpty()) {
            return forResult(null);
        }
        Iterator<? extends Task<?>> it = collection.iterator();
        while (it.hasNext()) {
            if (it.next() == null) {
                throw new NullPointerException("null tasks are not accepted");
            }
        }
        zzn zznVar = new zzn();
        zzc zzcVar = new zzc(collection.size(), zznVar);
        Iterator<? extends Task<?>> it2 = collection.iterator();
        while (it2.hasNext()) {
            zza(it2.next(), zzcVar);
        }
        return zznVar;
    }

    public static Task<Void> whenAll(Task<?>... taskArr) {
        return taskArr.length == 0 ? forResult(null) : whenAll(Arrays.asList(taskArr));
    }

    private static void zza(Task<?> task, zzb zzbVar) {
        task.addOnSuccessListener(TaskExecutors.zzbMf, zzbVar);
        task.addOnFailureListener(TaskExecutors.zzbMf, zzbVar);
    }

    private static <TResult> TResult zzb(Task<TResult> task) throws ExecutionException {
        if (task.isSuccessful()) {
            return task.getResult();
        }
        throw new ExecutionException(task.getException());
    }
}
