package com.google.android.gms.common.api;

import com.google.android.gms.common.internal.zzbo;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
public final class BatchResult implements Result {
    private final Status mStatus;
    private final PendingResult<?>[] zzaAF;

    BatchResult(Status status, PendingResult<?>[] pendingResultArr) {
        this.mStatus = status;
        this.zzaAF = pendingResultArr;
    }

    @Override // com.google.android.gms.common.api.Result
    public final Status getStatus() {
        return this.mStatus;
    }

    public final <R extends Result> R take(BatchResultToken<R> batchResultToken) {
        zzbo.zzb(batchResultToken.mId < this.zzaAF.length, "The result token does not belong to this batch");
        return (R) this.zzaAF[batchResultToken.mId].await(0L, TimeUnit.MILLISECONDS);
    }
}
