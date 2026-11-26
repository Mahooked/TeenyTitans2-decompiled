package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzabx implements zzrd {
    private /* synthetic */ zzabu zzUR;

    zzabx(zzabu zzabuVar) {
        this.zzUR = zzabuVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        synchronized (this.zzUR.mLock) {
            if (this.zzUR.zzUM.isDone()) {
                return;
            }
            zzaca zzacaVar = new zzaca(-2, map);
            if (this.zzUR.zzQx.equals(zzacaVar.getRequestId())) {
                this.zzUR.zzUM.zzg(zzacaVar);
            }
        }
    }
}
