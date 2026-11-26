package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zznu;

/* JADX INFO: loaded from: classes.dex */
final class zzbf implements Runnable {
    private /* synthetic */ zzaff zztx;
    private /* synthetic */ zzbb zzuQ;
    private /* synthetic */ String zzuR;

    zzbf(zzbb zzbbVar, String str, zzaff zzaffVar) {
        this.zzuQ = zzbbVar;
        this.zzuR = str;
        this.zztx = zzaffVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzuQ.zzsP.zzwi.get(this.zzuR).zza((zznu) this.zztx.zzXT);
        } catch (RemoteException e) {
            zzafr.zzc("Could not call onCustomTemplateAdLoadedListener.onCustomTemplateAdLoaded().", e);
        }
    }
}
