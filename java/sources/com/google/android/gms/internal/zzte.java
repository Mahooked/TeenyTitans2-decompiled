package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzte implements zzth {
    zzte(zzsy zzsyVar) {
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKl != null) {
            zztiVar.zzKl.onRewardedVideoAdLeftApplication();
        }
    }
}
