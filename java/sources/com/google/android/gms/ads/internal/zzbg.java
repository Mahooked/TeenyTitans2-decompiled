package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzpj;

/* JADX INFO: loaded from: classes.dex */
final class zzbg implements Runnable {
    private /* synthetic */ zzpj zztl;
    private /* synthetic */ zzbb zzuQ;

    zzbg(zzbb zzbbVar, zzpj zzpjVar) {
        this.zzuQ = zzbbVar;
        this.zztl = zzpjVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzuQ.zzsP.zzwi.get(this.zztl.getCustomTemplateId()).zza(this.zztl);
        } catch (RemoteException e) {
            zzafr.zzc("Could not call onCustomTemplateAdLoadedListener.onCustomTemplateAdLoaded().", e);
        }
    }
}
