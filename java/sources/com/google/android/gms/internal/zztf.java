package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zztf implements zzth {
    private /* synthetic */ int zzKc;

    zztf(zzsy zzsyVar, int i) {
        this.zzKc = i;
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKl != null) {
            zztiVar.zzKl.onRewardedVideoAdFailedToLoad(this.zzKc);
        }
    }
}
