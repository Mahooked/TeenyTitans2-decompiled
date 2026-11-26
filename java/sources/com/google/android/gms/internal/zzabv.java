package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzabv implements zzrd {
    private /* synthetic */ zzabu zzUR;

    zzabv(zzabu zzabuVar) {
        this.zzUR = zzabuVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        synchronized (this.zzUR.mLock) {
            if (this.zzUR.zzUM.isDone()) {
                return;
            }
            if (this.zzUR.zzQx.equals(map.get("request_id"))) {
                zzaca zzacaVar = new zzaca(1, map);
                String strValueOf = String.valueOf(zzacaVar.getType());
                String strValueOf2 = String.valueOf(zzacaVar.zzgH());
                zzafr.zzaT(new StringBuilder(String.valueOf(strValueOf).length() + 24 + String.valueOf(strValueOf2).length()).append("Invalid ").append(strValueOf).append(" request error: ").append(strValueOf2).toString());
                this.zzUR.zzUM.zzg(zzacaVar);
            }
        }
    }
}
