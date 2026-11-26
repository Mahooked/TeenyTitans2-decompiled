package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class zzabh implements zzrd {
    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("request_id");
        String strValueOf = String.valueOf(map.get("errors"));
        zzafr.zzaT(strValueOf.length() != 0 ? "Invalid request: ".concat(strValueOf) : new String("Invalid request: "));
        zzaaz.zzTZ.zzT(str);
    }
}
