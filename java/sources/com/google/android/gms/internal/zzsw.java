package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzsw extends zzjm {
    private /* synthetic */ zzsj zzKb;

    zzsw(zzsj zzsjVar) {
        this.zzKb = zzsjVar;
    }

    @Override // com.google.android.gms.internal.zzjl
    public final void onAdClicked() throws RemoteException {
        this.zzKb.zztH.add(new zzsx(this));
    }
}
