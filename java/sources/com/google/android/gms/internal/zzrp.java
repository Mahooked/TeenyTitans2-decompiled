package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrp implements zzrd {
    private static Map<String, Integer> zzJG = com.google.android.gms.common.util.zzf.zza("resize", 1, "playVideo", 2, "storePicture", 3, "createCalendarEvent", 4, "setOrientationProperties", 5, "closeResizedAd", 6);
    private final com.google.android.gms.ads.internal.zzw zzJE;
    private final zzwk zzJF;

    public zzrp(com.google.android.gms.ads.internal.zzw zzwVar, zzwk zzwkVar) {
        this.zzJE = zzwVar;
        this.zzJF = zzwkVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        int iIntValue = zzJG.get(map.get("a")).intValue();
        if (iIntValue != 5 && this.zzJE != null && !this.zzJE.zzaR()) {
            this.zzJE.zzt(null);
        }
        switch (iIntValue) {
            case 1:
                this.zzJF.execute(map);
                break;
            case 2:
            default:
                zzafr.zzaS("Unknown MRAID command called.");
                break;
            case 3:
                new zzwn(zzakaVar, map).execute();
                break;
            case 4:
                new zzwh(zzakaVar, map).execute();
                break;
            case 5:
                new zzwm(zzakaVar, map).execute();
                break;
            case 6:
                this.zzJF.zzk(true);
                break;
        }
    }
}
