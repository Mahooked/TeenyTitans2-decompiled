package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzabr implements Runnable {
    private /* synthetic */ zzaae zzUG;
    private /* synthetic */ zzaap zzUH;
    private /* synthetic */ zzabm zzUI;

    zzabr(zzabm zzabmVar, zzaae zzaaeVar, zzaap zzaapVar) {
        this.zzUI = zzabmVar;
        this.zzUG = zzaaeVar;
        this.zzUH = zzaapVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzaai zzaaiVar;
        try {
            zzaaiVar = this.zzUI.zzc(this.zzUG);
        } catch (Exception e) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "AdRequestServiceImpl.loadAdAsync");
            zzafr.zzc("Could not fetch ad response due to an Exception.", e);
            zzaaiVar = null;
        }
        if (zzaaiVar == null) {
            zzaaiVar = new zzaai(0);
        }
        try {
            this.zzUH.zza(zzaaiVar);
        } catch (RemoteException e2) {
            zzafr.zzc("Fail to forward ad response.", e2);
        }
    }
}
