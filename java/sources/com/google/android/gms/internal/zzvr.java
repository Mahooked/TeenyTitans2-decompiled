package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzvr implements Runnable {
    private /* synthetic */ zzvp zzNk;

    zzvr(zzvp zzvpVar) {
        this.zzNk = zzvpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdOpened();
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdOpened.", e);
        }
    }
}
