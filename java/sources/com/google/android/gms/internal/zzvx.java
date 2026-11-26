package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzvx implements Runnable {
    private /* synthetic */ zzvp zzNk;

    zzvx(zzvp zzvpVar) {
        this.zzNk = zzvpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdLoaded();
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdLoaded.", e);
        }
    }
}
