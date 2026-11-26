package com.google.android.gms.common.api;

/* JADX INFO: loaded from: classes.dex */
final class zzb implements PendingResult.zza {
    private /* synthetic */ Batch zzaAG;

    zzb(Batch batch) {
        this.zzaAG = batch;
    }

    @Override // com.google.android.gms.common.api.PendingResult.zza
    public final void zzo(Status status) {
        synchronized (this.zzaAG.mLock) {
            if (this.zzaAG.isCanceled()) {
                return;
            }
            if (status.isCanceled()) {
                Batch.zza(this.zzaAG, true);
            } else if (!status.isSuccess()) {
                Batch.zzb(this.zzaAG, true);
            }
            Batch.zzb(this.zzaAG);
            if (this.zzaAG.zzaAC == 0) {
                if (this.zzaAG.zzaAE) {
                    super/*com.google.android.gms.internal.zzbbe*/.cancel();
                } else {
                    this.zzaAG.setResult(new BatchResult(this.zzaAG.zzaAD ? new Status(13) : Status.zzaBm, this.zzaAG.zzaAF));
                }
            }
        }
    }
}
