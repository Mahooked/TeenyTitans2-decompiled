package com.google.android.gms.common.internal;

import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
final class zzbj implements PendingResult.zza {
    private /* synthetic */ PendingResult zzaIj;
    private /* synthetic */ TaskCompletionSource zzaIk;
    private /* synthetic */ zzbm zzaIl;
    private /* synthetic */ zzbn zzaIm;

    zzbj(PendingResult pendingResult, TaskCompletionSource taskCompletionSource, zzbm zzbmVar, zzbn zzbnVar) {
        this.zzaIj = pendingResult;
        this.zzaIk = taskCompletionSource;
        this.zzaIl = zzbmVar;
        this.zzaIm = zzbnVar;
    }

    @Override // com.google.android.gms.common.api.PendingResult.zza
    public final void zzo(Status status) {
        if (!status.isSuccess()) {
            this.zzaIk.setException(this.zzaIm.zzy(status));
        } else {
            this.zzaIk.setResult(this.zzaIl.zzd(this.zzaIj.await(0L, TimeUnit.MILLISECONDS)));
        }
    }
}
