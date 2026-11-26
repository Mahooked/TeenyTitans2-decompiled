package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsv implements zzth {
    private /* synthetic */ zzne zzKe;

    zzsv(zzsu zzsuVar, zzne zzneVar) {
        this.zzKe = zzneVar;
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKj != null) {
            zztiVar.zzKj.zza(this.zzKe);
        }
    }
}
