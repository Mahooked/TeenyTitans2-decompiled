package com.google.android.gms.common.api;

import com.google.android.gms.internal.zzbbe;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class Batch extends zzbbe<BatchResult> {
    private final Object mLock;
    private int zzaAC;
    private boolean zzaAD;
    private boolean zzaAE;
    private final PendingResult<?>[] zzaAF;

    public static final class Builder {
        private List<PendingResult<?>> zzaAH = new ArrayList();
        private GoogleApiClient zzapu;

        public Builder(GoogleApiClient googleApiClient) {
            this.zzapu = googleApiClient;
        }

        public final <R extends Result> BatchResultToken<R> add(PendingResult<R> pendingResult) {
            BatchResultToken<R> batchResultToken = new BatchResultToken<>(this.zzaAH.size());
            this.zzaAH.add(pendingResult);
            return batchResultToken;
        }

        public final Batch build() {
            return new Batch(this.zzaAH, this.zzapu, null);
        }
    }

    private Batch(List<PendingResult<?>> list, GoogleApiClient googleApiClient) {
        super(googleApiClient);
        this.mLock = new Object();
        this.zzaAC = list.size();
        this.zzaAF = new PendingResult[this.zzaAC];
        if (list.isEmpty()) {
            setResult(new BatchResult(Status.zzaBm, this.zzaAF));
            return;
        }
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= list.size()) {
                return;
            }
            PendingResult<?> pendingResult = list.get(i2);
            this.zzaAF[i2] = pendingResult;
            pendingResult.zza(new zzb(this));
            i = i2 + 1;
        }
    }

    /* synthetic */ Batch(List list, GoogleApiClient googleApiClient, zzb zzbVar) {
        this(list, googleApiClient);
    }

    static /* synthetic */ boolean zza(Batch batch, boolean z) {
        batch.zzaAE = true;
        return true;
    }

    static /* synthetic */ int zzb(Batch batch) {
        int i = batch.zzaAC;
        batch.zzaAC = i - 1;
        return i;
    }

    static /* synthetic */ boolean zzb(Batch batch, boolean z) {
        batch.zzaAD = true;
        return true;
    }

    @Override // com.google.android.gms.internal.zzbbe, com.google.android.gms.common.api.PendingResult
    public final void cancel() {
        super.cancel();
        for (PendingResult<?> pendingResult : this.zzaAF) {
            pendingResult.cancel();
        }
    }

    @Override // com.google.android.gms.internal.zzbbe
    /* JADX INFO: renamed from: createFailedResult, reason: merged with bridge method [inline-methods] */
    public final BatchResult zzb(Status status) {
        return new BatchResult(status, this.zzaAF);
    }
}
