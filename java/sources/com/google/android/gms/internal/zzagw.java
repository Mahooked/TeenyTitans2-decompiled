package com.google.android.gms.internal;

import android.os.Process;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzagw implements Runnable {
    private /* synthetic */ zzajg zzZn;
    private /* synthetic */ Callable zzZo;

    zzagw(zzajg zzajgVar, Callable callable) {
        this.zzZn = zzajgVar;
        this.zzZo = callable;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            Process.setThreadPriority(10);
            this.zzZn.zzg(this.zzZo.call());
        } catch (Exception e) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "AdThreadPool.submit");
            this.zzZn.zzb(e);
        }
    }
}
