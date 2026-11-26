package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzvq implements Runnable {
    private /* synthetic */ zzvp zzNk;

    zzvq(zzvp zzvpVar) {
        this.zzNk = zzvpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdClicked();
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdClicked.", e);
        }
    }
}
