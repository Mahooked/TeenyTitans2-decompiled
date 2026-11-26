package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqr implements zzrd {
    zzqr() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("action");
        if ("pause".equals(str)) {
            zzakaVar.zzaJ();
        } else if ("resume".equals(str)) {
            zzakaVar.zzaK();
        }
    }
}
