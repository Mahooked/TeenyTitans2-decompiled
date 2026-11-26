package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsm implements zzth {
    private /* synthetic */ int zzKc;

    zzsm(zzsk zzskVar, int i) {
        this.zzKc = i;
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zztK != null) {
            zztiVar.zztK.onAdFailedToLoad(this.zzKc);
        }
    }
}
