package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzvt implements Runnable {
    private /* synthetic */ zzvp zzNk;

    zzvt(zzvp zzvpVar) {
        this.zzNk = zzvpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdClosed();
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdClosed.", e);
        }
    }
}
