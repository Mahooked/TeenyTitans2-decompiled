package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zztg implements Runnable {
    private /* synthetic */ zzth zzKg;
    private /* synthetic */ zzti zzKh;

    zztg(zzsj zzsjVar, zzth zzthVar, zzti zztiVar) {
        this.zzKg = zzthVar;
        this.zzKh = zztiVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzKg.zzb(this.zzKh);
        } catch (RemoteException e) {
            zzafr.zzc("Could not propagate interstitial ad event.", e);
        }
    }
}
