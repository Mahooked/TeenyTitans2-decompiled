package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzir;

/* JADX INFO: loaded from: classes.dex */
final class zzaj implements Runnable {
    private /* synthetic */ zzir zztY;
    private /* synthetic */ zzai zztZ;

    zzaj(zzai zzaiVar, zzir zzirVar) {
        this.zztZ = zzaiVar;
        this.zztY = zzirVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zztZ.mLock) {
            if (this.zztZ.zzaW()) {
                this.zztZ.zzd(this.zztY);
            } else {
                this.zztZ.zze(this.zztY);
            }
        }
    }
}
