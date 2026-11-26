package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzns;

/* JADX INFO: loaded from: classes.dex */
final class zzbe implements Runnable {
    private /* synthetic */ zzns zztk;
    private /* synthetic */ zzbb zzuQ;

    zzbe(zzbb zzbbVar, zzns zznsVar) {
        this.zzuQ = zzbbVar;
        this.zztk = zznsVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            if (this.zzuQ.zzsP.zzwg != null) {
                this.zzuQ.zzsP.zzwg.zza(this.zztk);
            }
        } catch (RemoteException e) {
            zzafr.zzc("Could not call OnContentAdLoadedListener.onContentAdLoaded().", e);
        }
    }
}
