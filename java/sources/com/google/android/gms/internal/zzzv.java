package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzzv implements zzzx {
    private /* synthetic */ Context zztF;

    zzzv(Context context) {
        this.zztF = context;
    }

    @Override // com.google.android.gms.internal.zzzx
    public final boolean zza(zzaje zzajeVar) {
        zzji.zzds();
        boolean zZzX = zzaiy.zzX(this.zztF);
        boolean z = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGF)).booleanValue() && zzajeVar.zzaaQ;
        if (zzzu.zzc(this.zztF, zzajeVar.zzaaQ) && zZzX && !z) {
            if (!com.google.android.gms.common.util.zzj.zzaI(this.zztF)) {
                return false;
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCL)).booleanValue()) {
                return false;
            }
        }
        return true;
    }
}
