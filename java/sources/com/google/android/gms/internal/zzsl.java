package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsl implements zzth {
    zzsl(zzsk zzskVar) {
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zztK != null) {
            zztiVar.zztK.onAdClosed();
        }
    }
}
