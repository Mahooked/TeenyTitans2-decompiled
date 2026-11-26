package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

/* JADX INFO: loaded from: classes.dex */
final class zzg<TResult> implements zzk<TResult> {
    private final Object mLock = new Object();
    private final Executor zzbEo;
    private OnFailureListener zzbLY;

    public zzg(@NonNull Executor executor, @NonNull OnFailureListener onFailureListener) {
        this.zzbEo = executor;
        this.zzbLY = onFailureListener;
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void cancel() {
        synchronized (this.mLock) {
            this.zzbLY = null;
        }
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void onComplete(@NonNull Task<TResult> task) {
        if (task.isSuccessful()) {
            return;
        }
        synchronized (this.mLock) {
            if (this.zzbLY != null) {
                this.zzbEo.execute(new zzh(this, task));
            }
        }
    }
}
