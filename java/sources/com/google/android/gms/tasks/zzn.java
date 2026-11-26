package com.google.android.gms.tasks;

import android.app.Activity;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.internal.zzbds;
import com.google.android.gms.internal.zzbdt;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.Executor;

/* JADX INFO: loaded from: classes.dex */
final class zzn<TResult> extends Task<TResult> {
    private final Object mLock = new Object();
    private final zzl<TResult> zzbMg = new zzl<>();
    private boolean zzbMh;
    private TResult zzbMi;
    private Exception zzbMj;

    static class zza extends zzbds {
        private final List<WeakReference<zzk<?>>> mListeners;

        private zza(zzbdt zzbdtVar) {
            super(zzbdtVar);
            this.mListeners = new ArrayList();
            this.zzaEG.zza("TaskOnStopCallback", this);
        }

        public static zza zzr(Activity activity) {
            zzbdt zzbdtVarZzn = zzn(activity);
            zza zzaVar = (zza) zzbdtVarZzn.zza("TaskOnStopCallback", zza.class);
            return zzaVar == null ? new zza(zzbdtVarZzn) : zzaVar;
        }

        @Override // com.google.android.gms.internal.zzbds
        @MainThread
        public void onStop() {
            synchronized (this.mListeners) {
                Iterator<WeakReference<zzk<?>>> it = this.mListeners.iterator();
                while (it.hasNext()) {
                    zzk<?> zzkVar = it.next().get();
                    if (zzkVar != null) {
                        zzkVar.cancel();
                    }
                }
                this.mListeners.clear();
            }
        }

        public final <T> void zzb(zzk<T> zzkVar) {
            synchronized (this.mListeners) {
                this.mListeners.add(new WeakReference<>(zzkVar));
            }
        }
    }

    zzn() {
    }

    private final void zzDG() {
        zzbo.zza(this.zzbMh, "Task is not yet complete");
    }

    private final void zzDH() {
        zzbo.zza(!this.zzbMh, "Task is already complete");
    }

    private final void zzDI() {
        synchronized (this.mLock) {
            if (this.zzbMh) {
                this.zzbMg.zza(this);
            }
        }
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull Activity activity, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        zze zzeVar = new zze(TaskExecutors.MAIN_THREAD, onCompleteListener);
        this.zzbMg.zza(zzeVar);
        zza.zzr(activity).zzb(zzeVar);
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull OnCompleteListener<TResult> onCompleteListener) {
        return addOnCompleteListener(TaskExecutors.MAIN_THREAD, onCompleteListener);
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull Executor executor, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        this.zzbMg.zza(new zze(executor, onCompleteListener));
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull Activity activity, @NonNull OnFailureListener onFailureListener) {
        zzg zzgVar = new zzg(TaskExecutors.MAIN_THREAD, onFailureListener);
        this.zzbMg.zza(zzgVar);
        zza.zzr(activity).zzb(zzgVar);
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull OnFailureListener onFailureListener) {
        return addOnFailureListener(TaskExecutors.MAIN_THREAD, onFailureListener);
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull Executor executor, @NonNull OnFailureListener onFailureListener) {
        this.zzbMg.zza(new zzg(executor, onFailureListener));
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull Activity activity, @NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        zzi zziVar = new zzi(TaskExecutors.MAIN_THREAD, onSuccessListener);
        this.zzbMg.zza(zziVar);
        zza.zzr(activity).zzb(zziVar);
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        return addOnSuccessListener(TaskExecutors.MAIN_THREAD, onSuccessListener);
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull Executor executor, @NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        this.zzbMg.zza(new zzi(executor, onSuccessListener));
        zzDI();
        return this;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWith(@NonNull Continuation<TResult, TContinuationResult> continuation) {
        return continueWith(TaskExecutors.MAIN_THREAD, continuation);
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWith(@NonNull Executor executor, @NonNull Continuation<TResult, TContinuationResult> continuation) {
        zzn zznVar = new zzn();
        this.zzbMg.zza(new com.google.android.gms.tasks.zza(executor, continuation, zznVar));
        zzDI();
        return zznVar;
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWithTask(@NonNull Continuation<TResult, Task<TContinuationResult>> continuation) {
        return continueWithTask(TaskExecutors.MAIN_THREAD, continuation);
    }

    @Override // com.google.android.gms.tasks.Task
    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWithTask(@NonNull Executor executor, @NonNull Continuation<TResult, Task<TContinuationResult>> continuation) {
        zzn zznVar = new zzn();
        this.zzbMg.zza(new zzc(executor, continuation, zznVar));
        zzDI();
        return zznVar;
    }

    @Override // com.google.android.gms.tasks.Task
    @Nullable
    public final Exception getException() {
        Exception exc;
        synchronized (this.mLock) {
            exc = this.zzbMj;
        }
        return exc;
    }

    @Override // com.google.android.gms.tasks.Task
    public final TResult getResult() {
        TResult tresult;
        synchronized (this.mLock) {
            zzDG();
            if (this.zzbMj != null) {
                throw new RuntimeExecutionException(this.zzbMj);
            }
            tresult = this.zzbMi;
        }
        return tresult;
    }

    @Override // com.google.android.gms.tasks.Task
    public final <X extends Throwable> TResult getResult(@NonNull Class<X> cls) throws Throwable {
        TResult tresult;
        synchronized (this.mLock) {
            zzDG();
            if (cls.isInstance(this.zzbMj)) {
                throw cls.cast(this.zzbMj);
            }
            if (this.zzbMj != null) {
                throw new RuntimeExecutionException(this.zzbMj);
            }
            tresult = this.zzbMi;
        }
        return tresult;
    }

    @Override // com.google.android.gms.tasks.Task
    public final boolean isComplete() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzbMh;
        }
        return z;
    }

    @Override // com.google.android.gms.tasks.Task
    public final boolean isSuccessful() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzbMh && this.zzbMj == null;
        }
        return z;
    }

    public final void setException(@NonNull Exception exc) {
        zzbo.zzb(exc, "Exception must not be null");
        synchronized (this.mLock) {
            zzDH();
            this.zzbMh = true;
            this.zzbMj = exc;
        }
        this.zzbMg.zza(this);
    }

    public final void setResult(TResult tresult) {
        synchronized (this.mLock) {
            zzDH();
            this.zzbMh = true;
            this.zzbMi = tresult;
        }
        this.zzbMg.zza(this);
    }

    public final boolean trySetException(@NonNull Exception exc) {
        boolean z = true;
        zzbo.zzb(exc, "Exception must not be null");
        synchronized (this.mLock) {
            if (this.zzbMh) {
                z = false;
            } else {
                this.zzbMh = true;
                this.zzbMj = exc;
                this.zzbMg.zza(this);
            }
        }
        return z;
    }

    public final boolean trySetResult(TResult tresult) {
        boolean z = true;
        synchronized (this.mLock) {
            if (this.zzbMh) {
                z = false;
            } else {
                this.zzbMh = true;
                this.zzbMi = tresult;
                this.zzbMg.zza(this);
            }
        }
        return z;
    }
}
