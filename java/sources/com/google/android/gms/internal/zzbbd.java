package com.google.android.gms.internal;

import android.app.Dialog;

/* JADX INFO: loaded from: classes.dex */
final class zzbbd extends zzbdl {
    private /* synthetic */ Dialog zzaBT;
    private /* synthetic */ zzbbc zzaBU;

    zzbbd(zzbbc zzbbcVar, Dialog dialog) {
        this.zzaBU = zzbbcVar;
        this.zzaBT = dialog;
    }

    @Override // com.google.android.gms.internal.zzbdl
    public final void zzpA() {
        this.zzaBU.zzaBS.zzpx();
        if (this.zzaBT.isShowing()) {
            this.zzaBT.dismiss();
        }
    }
}
