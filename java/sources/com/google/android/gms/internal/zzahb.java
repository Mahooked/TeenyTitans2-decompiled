package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzahb implements Runnable {
    private /* synthetic */ zzagz zzZy;
    private /* synthetic */ Context zztF;

    zzahb(zzagz zzagzVar, Context context) {
        this.zzZy = zzagzVar;
        this.zztF = context;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzZy.mLock) {
            this.zzZy.zzJP = zzagz.zzF(this.zztF);
            this.zzZy.mLock.notifyAll();
        }
    }
}
