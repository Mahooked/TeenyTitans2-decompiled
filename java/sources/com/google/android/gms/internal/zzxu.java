package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzxu implements Runnable {
    private /* synthetic */ zzxt zzQW;

    zzxu(zzxt zzxtVar) {
        this.zzQW = zzxtVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzQW.onStop();
    }
}
