package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzst implements zzth {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zzKd;

    zzst(zzss zzssVar, String str, String str2) {
        this.val$name = str;
        this.zzKd = str2;
    }

    @Override // com.google.android.gms.internal.zzth
    public final void zzb(zzti zztiVar) throws RemoteException {
        if (zztiVar.zzKi != null) {
            zztiVar.zzKi.onAppEvent(this.val$name, this.zzKd);
        }
    }
}
