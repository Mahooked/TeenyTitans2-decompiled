package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzra implements zzrd {
    zzra() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("u");
        if (str == null) {
            zzafr.zzaT("URL missing from httpTrack GMSG.");
        } else {
            new zzaiq(zzakaVar.getContext(), zzakaVar.zziz().zzaP, str).zzhL();
        }
    }
}
