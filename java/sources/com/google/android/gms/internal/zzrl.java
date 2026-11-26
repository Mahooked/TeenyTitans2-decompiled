package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrl implements zzrd {
    private final zzrm zzJC;

    public zzrl(zzrm zzrmVar) {
        this.zzJC = zzrmVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        float f;
        boolean zEquals = "1".equals(map.get("transparentBackground"));
        boolean zEquals2 = "1".equals(map.get("blur"));
        try {
            f = map.get("blurRadius") != null ? Float.parseFloat(map.get("blurRadius")) : 0.0f;
        } catch (NumberFormatException e) {
            zzafr.zzb("Fail to parse float", e);
        }
        this.zzJC.zzc(zEquals);
        this.zzJC.zza(zEquals2, f);
    }
}
