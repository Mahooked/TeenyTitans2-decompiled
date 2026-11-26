package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzlo implements Runnable {
    private /* synthetic */ zzln zzBt;

    zzlo(zzln zzlnVar) {
        this.zzBt = zzlnVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzBt.zztK != null) {
            try {
                this.zzBt.zztK.onAdFailedToLoad(1);
            } catch (RemoteException e) {
                zzajc.zzc("Could not notify onAdFailedToLoad event.", e);
            }
        }
    }
}
