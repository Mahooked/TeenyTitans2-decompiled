package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbdc implements zzbax {
    private /* synthetic */ zzbdb zzaEm;

    zzbdc(zzbdb zzbdbVar) {
        this.zzaEm = zzbdbVar;
    }

    @Override // com.google.android.gms.internal.zzbax
    public final void zzac(boolean z) {
        this.zzaEm.mHandler.sendMessage(this.zzaEm.mHandler.obtainMessage(1, Boolean.valueOf(z)));
    }
}
