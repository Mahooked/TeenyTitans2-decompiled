package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.ads.AdRequest;

/* JADX INFO: loaded from: classes.dex */
final class zzvz implements Runnable {
    private /* synthetic */ zzvp zzNk;
    private /* synthetic */ AdRequest.ErrorCode zzNl;

    zzvz(zzvp zzvpVar, AdRequest.ErrorCode errorCode) {
        this.zzNk = zzvpVar;
        this.zzNl = errorCode;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzNk.zzNc.onAdFailedToLoad(zzwb.zza(this.zzNl));
        } catch (RemoteException e) {
            zzajc.zzc("Could not call onAdFailedToLoad.", e);
        }
    }
}
