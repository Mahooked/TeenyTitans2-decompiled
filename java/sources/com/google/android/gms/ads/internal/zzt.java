package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzns;

/* JADX INFO: loaded from: classes.dex */
final class zzt implements Runnable {
    private /* synthetic */ zzq zzti;
    private /* synthetic */ zzns zztk;

    zzt(zzq zzqVar, zzns zznsVar) {
        this.zzti = zzqVar;
        this.zztk = zznsVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            if (this.zzti.zzsP.zzwg != null) {
                this.zzti.zzsP.zzwg.zza(this.zztk);
            }
        } catch (RemoteException e) {
            zzafr.zzc("Could not call OnContentAdLoadedListener.onContentAdLoaded().", e);
        }
    }
}
