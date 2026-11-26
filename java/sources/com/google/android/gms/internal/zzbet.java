package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;

/* JADX INFO: loaded from: classes.dex */
final class zzbet implements Runnable {
    private /* synthetic */ Result zzaFh;
    private /* synthetic */ zzbes zzaFi;

    zzbet(zzbes zzbesVar, Result result) {
        this.zzaFi = zzbesVar;
        this.zzaFh = result;
    }

    @Override // java.lang.Runnable
    @WorkerThread
    public final void run() {
        try {
            zzbbe.zzaBV.set(true);
            this.zzaFi.zzaFf.sendMessage(this.zzaFi.zzaFf.obtainMessage(0, this.zzaFi.zzaFa.onSuccess(this.zzaFh)));
        } catch (RuntimeException e) {
            this.zzaFi.zzaFf.sendMessage(this.zzaFi.zzaFf.obtainMessage(1, e));
        } finally {
            zzbbe.zzaBV.set(false);
            zzbes zzbesVar = this.zzaFi;
            zzbes.zzc(this.zzaFh);
            GoogleApiClient googleApiClient = (GoogleApiClient) this.zzaFi.zzaBY.get();
            if (googleApiClient != null) {
                googleApiClient.zzb(this.zzaFi);
            }
        }
    }
}
