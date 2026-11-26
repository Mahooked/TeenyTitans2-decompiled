package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzuo implements Runnable {
    private /* synthetic */ zzuh zzMX;

    zzuo(zzun zzunVar, zzuh zzuhVar) {
        this.zzMX = zzuhVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzMX.zzMH.destroy();
        } catch (RemoteException e) {
            zzafr.zzc("Could not destroy mediation adapter.", e);
        }
    }
}
