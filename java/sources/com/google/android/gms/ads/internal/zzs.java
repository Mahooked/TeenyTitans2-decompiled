package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zznq;

/* JADX INFO: loaded from: classes.dex */
final class zzs implements Runnable {
    private /* synthetic */ zzq zzti;
    private /* synthetic */ zznq zztj;

    zzs(zzq zzqVar, zznq zznqVar) {
        this.zzti = zzqVar;
        this.zztj = zznqVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            if (this.zzti.zzsP.zzwf != null) {
                this.zzti.zzsP.zzwf.zza(this.zztj);
            }
        } catch (RemoteException e) {
            zzafr.zzc("Could not call OnAppInstallAdLoadedListener.onAppInstallAdLoaded().", e);
        }
    }
}
