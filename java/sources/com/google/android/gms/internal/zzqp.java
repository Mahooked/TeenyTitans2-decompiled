package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqp implements zzrd {
    zzqp() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("tx");
        String str2 = map.get("ty");
        String str3 = map.get("td");
        try {
            int i = Integer.parseInt(str);
            int i2 = Integer.parseInt(str2);
            int i3 = Integer.parseInt(str3);
            zzcu zzcuVarZziy = zzakaVar.zziy();
            if (zzcuVarZziy != null) {
                zzcuVarZziy.zzB().zza(i, i2, i3);
            }
        } catch (NumberFormatException e) {
            zzafr.zzaT("Could not parse touch parameters from gmsg.");
        }
    }
}
