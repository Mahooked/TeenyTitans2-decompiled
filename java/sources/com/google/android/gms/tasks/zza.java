package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

/* JADX INFO: loaded from: classes.dex */
final class zza<TResult, TContinuationResult> implements zzk<TResult> {
    private final Executor zzbEo;
    private final Continuation<TResult, TContinuationResult> zzbLR;
    private final zzn<TContinuationResult> zzbLS;

    public zza(@NonNull Executor executor, @NonNull Continuation<TResult, TContinuationResult> continuation, @NonNull zzn<TContinuationResult> zznVar) {
        this.zzbEo = executor;
        this.zzbLR = continuation;
        this.zzbLS = zznVar;
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void cancel() {
        throw new UnsupportedOperationException();
    }

    @Override // com.google.android.gms.tasks.zzk
    public final void onComplete(@NonNull Task<TResult> task) {
        this.zzbEo.execute(new zzb(this, task));
    }
}
