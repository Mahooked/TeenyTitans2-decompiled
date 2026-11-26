package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzyh;

/* JADX INFO: loaded from: classes.dex */
final class zzbh implements Runnable {
    private /* synthetic */ zzbb zzuQ;

    zzbh(zzbb zzbbVar) {
        this.zzuQ = zzbbVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzuQ.mLock) {
            zzyh zzyhVarZzbi = this.zzuQ.zzbi();
            if (zzyhVarZzbi != null) {
                zzyhVarZzbi.zzfd();
            }
        }
    }
}
