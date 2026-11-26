package com.google.android.gms.internal;

import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzul implements Callable<zzuh> {
    private /* synthetic */ zzue zzMT;
    private /* synthetic */ zzuk zzMU;

    zzul(zzuk zzukVar, zzue zzueVar) {
        this.zzMU = zzukVar;
        this.zzMT = zzueVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // java.util.concurrent.Callable
    /* JADX INFO: renamed from: zzfp, reason: merged with bridge method [inline-methods] */
    public final zzuh call() throws Exception {
        synchronized (this.zzMU.mLock) {
            if (this.zzMU.zzMP) {
                return null;
            }
            return this.zzMT.zza(this.zzMU.mStartTime, this.zzMU.zzMN);
        }
    }
}
