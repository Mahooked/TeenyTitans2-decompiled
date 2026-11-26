package com.google.android.gms.internal;

import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzbcv extends zzbdl {
    private WeakReference<zzbcp> zzaDR;

    zzbcv(zzbcp zzbcpVar) {
        this.zzaDR = new WeakReference<>(zzbcpVar);
    }

    @Override // com.google.android.gms.internal.zzbdl
    public final void zzpA() {
        zzbcp zzbcpVar = this.zzaDR.get();
        if (zzbcpVar == null) {
            return;
        }
        zzbcpVar.resume();
    }
}
