package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzvv implements Runnable {
    private /* synthetic */ zzvp zzNk;

    zzvv(zzvp zzvpVar) {
        this.zzNk = zzvpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdLeftApplication();
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdLeftApplication.", e);
        }
    }
}
