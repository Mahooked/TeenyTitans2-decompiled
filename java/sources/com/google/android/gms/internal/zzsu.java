package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsu extends zzni {
    private /* synthetic */ zzsj zzKb;

    zzsu(zzsj zzsjVar) {
        this.zzKb = zzsjVar;
    }

    @Override // com.google.android.gms.internal.zznh
    public final void zza(zzne zzneVar) throws RemoteException {
        this.zzKb.zztH.add(new zzsv(this, zzneVar));
    }
}
