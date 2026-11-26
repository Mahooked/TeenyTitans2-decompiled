package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzpj;

/* JADX INFO: loaded from: classes.dex */
final class zzu implements Runnable {
    private /* synthetic */ zzq zzti;
    private /* synthetic */ zzpj zztl;

    zzu(zzq zzqVar, zzpj zzpjVar) {
        this.zzti = zzqVar;
        this.zztl = zzpjVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzti.zzsP.zzwi.get(this.zztl.getCustomTemplateId()).zza(this.zztl);
        } catch (RemoteException e) {
            zzafr.zzc("Could not call onCustomTemplateAdLoadedListener.onCustomTemplateAdLoaded().", e);
        }
    }
}
