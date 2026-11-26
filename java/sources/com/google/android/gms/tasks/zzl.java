package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.ArrayDeque;
import java.util.Queue;

/* JADX INFO: loaded from: classes.dex */
final class zzl<TResult> {
    private final Object mLock = new Object();
    private Queue<zzk<TResult>> zzbMc;
    private boolean zzbMd;

    zzl() {
    }

    public final void zza(@NonNull Task<TResult> task) {
        zzk<TResult> zzkVarPoll;
        synchronized (this.mLock) {
            if (this.zzbMc == null || this.zzbMd) {
                return;
            }
            this.zzbMd = true;
            while (true) {
                synchronized (this.mLock) {
                    zzkVarPoll = this.zzbMc.poll();
                    if (zzkVarPoll == null) {
                        this.zzbMd = false;
                        return;
                    }
                }
                zzkVarPoll.onComplete(task);
            }
        }
    }

    public final void zza(@NonNull zzk<TResult> zzkVar) {
        synchronized (this.mLock) {
            if (this.zzbMc == null) {
                this.zzbMc = new ArrayDeque();
            }
            this.zzbMc.add(zzkVar);
        }
    }
}
