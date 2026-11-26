package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zznq;

/* JADX INFO: loaded from: classes.dex */
final class zzbd implements Runnable {
    private /* synthetic */ zznq zztj;
    private /* synthetic */ zzbb zzuQ;

    zzbd(zzbb zzbbVar, zznq zznqVar) {
        this.zzuQ = zzbbVar;
        this.zztj = zznqVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            if (this.zzuQ.zzsP.zzwf != null) {
                this.zzuQ.zzsP.zzwf.zza(this.zztj);
            }
        } catch (RemoteException e) {
            zzafr.zzc("Could not call OnAppInstallAdLoadedListener.onAppInstallAdLoaded().", e);
        }
    }
}
