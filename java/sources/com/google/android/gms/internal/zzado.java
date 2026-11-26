package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzado implements Runnable {
    private /* synthetic */ zzut zzWF;
    private /* synthetic */ zzadm zzWG;
    private /* synthetic */ zzadu zzWH;
    private /* synthetic */ zzir zztY;

    zzado(zzadm zzadmVar, zzut zzutVar, zzir zzirVar, zzadu zzaduVar) {
        this.zzWG = zzadmVar;
        this.zzWF = zzutVar;
        this.zztY = zzirVar;
        this.zzWH = zzaduVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzWF.zza(com.google.android.gms.dynamic.zzn.zzw(this.zzWG.mContext), this.zztY, (String) null, this.zzWH, this.zzWG.zzWA);
        } catch (RemoteException e) {
            String strValueOf = String.valueOf(this.zzWG.zzMs);
            zzafr.zzc(strValueOf.length() != 0 ? "Fail to initialize adapter ".concat(strValueOf) : new String("Fail to initialize adapter "), e);
            this.zzWG.zza(this.zzWG.zzMs, 0);
        }
    }
}
