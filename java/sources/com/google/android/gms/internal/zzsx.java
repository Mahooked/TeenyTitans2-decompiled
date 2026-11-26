package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsx implements zzth {
    zzsx(zzsw zzswVar) {
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKk != null) {
            zztiVar.zzKk.onAdClicked();
        }
    }
}
