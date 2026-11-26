package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: Add missing generic type declarations: [TResult] */
/* JADX INFO: loaded from: classes.dex */
final class zzbbv<TResult> implements OnCompleteListener<TResult> {
    private /* synthetic */ zzbbt zzaCU;
    private /* synthetic */ TaskCompletionSource zzaCV;

    zzbbv(zzbbt zzbbtVar, TaskCompletionSource taskCompletionSource) {
        this.zzaCU = zzbbtVar;
        this.zzaCV = taskCompletionSource;
    }

    @Override // com.google.android.gms.tasks.OnCompleteListener
    public final void onComplete(@NonNull Task<TResult> task) {
        this.zzaCU.zzaCS.remove(this.zzaCV);
    }
}
