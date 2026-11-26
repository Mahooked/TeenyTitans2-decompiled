package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzsf implements zzrd {
    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        int i;
        com.google.android.gms.ads.internal.zzbs.zzbW();
        if (map.containsKey("abort")) {
            if (zzsa.zze(zzakaVar)) {
                return;
            }
            zzafr.zzaT("Precache abort but no preload task running.");
            return;
        }
        String str = map.get("src");
        if (str == null) {
            zzafr.zzaT("Precache video action is missing the src parameter.");
            return;
        }
        try {
            i = Integer.parseInt(map.get("player"));
        } catch (NumberFormatException e) {
            i = 0;
        }
        String str2 = map.containsKey("mimetype") ? map.get("mimetype") : "";
        if (zzsa.zzf(zzakaVar)) {
            zzafr.zzaT("Precache task already running.");
        } else {
            com.google.android.gms.common.internal.zzc.zzr(zzakaVar.zzak());
            new zzry(zzakaVar, zzakaVar.zzak().zztm.zza(zzakaVar, i, str2), str).zzhL();
        }
    }
}
