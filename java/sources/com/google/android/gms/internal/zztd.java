package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zztd implements zzth {
    private /* synthetic */ zzacv zzKf;

    zztd(zzsy zzsyVar, zzacv zzacvVar) {
        this.zzKf = zzacvVar;
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKl != null) {
            zztiVar.zzKl.zza(this.zzKf);
        }
    }
}
