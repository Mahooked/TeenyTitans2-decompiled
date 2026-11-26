package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

/* JADX INFO: loaded from: classes.dex */
final class zze<TResult> implements zzk<TResult> {
    private final Object mLock = new Object();
    private final Executor zzbEo;
    private OnCompleteListener<TResult> zzbLW;

    public zze(@NonNull Executor executor, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        this.zzbEo = executor;
        this.zzbLW = onCompleteListener;
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void cancel() {
        synchronized (this.mLock) {
            this.zzbLW = null;
        }
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void onComplete(@NonNull Task<TResult> task) {
        synchronized (this.mLock) {
            if (this.zzbLW == null) {
                return;
            }
            this.zzbEo.execute(new zzf(this, task));
        }
    }
}
