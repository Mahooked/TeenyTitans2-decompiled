package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzss extends zzkf {
    private /* synthetic */ zzsj zzKb;

    zzss(zzsj zzsjVar) {
        this.zzKb = zzsjVar;
    }

    @Override // com.google.android.gms.internal.zzke
    public final void onAppEvent(String str, String str2) throws RemoteException {
        this.zzKb.zztH.add(new zzst(this, str, str2));
    }
}
