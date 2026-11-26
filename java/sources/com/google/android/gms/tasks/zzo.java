package com.google.android.gms.tasks;

import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzo implements Runnable {
    private /* synthetic */ Callable zzZo;
    private /* synthetic */ zzn zzbMk;

    zzo(zzn zznVar, Callable callable) {
        this.zzbMk = zznVar;
        this.zzZo = callable;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzbMk.setResult(this.zzZo.call());
        } catch (Exception e) {
            this.zzbMk.setException(e);
        }
    }
}
