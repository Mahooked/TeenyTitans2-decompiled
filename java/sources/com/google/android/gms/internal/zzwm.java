package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwm {
    private final zzaka zzJH;
    private final boolean zzNM;
    private final String zzNN;

    public zzwm(zzaka zzakaVar, Map<String, String> map) {
        this.zzJH = zzakaVar;
        this.zzNN = map.get("forceOrientation");
        if (map.containsKey("allowOrientationChange")) {
            this.zzNM = Boolean.parseBoolean(map.get("allowOrientationChange"));
        } else {
            this.zzNM = true;
        }
    }

    public final void execute() {
        int iZzhV;
        if (this.zzJH == null) {
            zzafr.zzaT("AdWebView is null");
            return;
        }
        if ("portrait".equalsIgnoreCase(this.zzNN)) {
            iZzhV = com.google.android.gms.ads.internal.zzbs.zzbB().zzhU();
        } else if ("landscape".equalsIgnoreCase(this.zzNN)) {
            iZzhV = com.google.android.gms.ads.internal.zzbs.zzbB().zzhT();
        } else {
            iZzhV = this.zzNM ? -1 : com.google.android.gms.ads.internal.zzbs.zzbB().zzhV();
        }
        this.zzJH.setRequestedOrientation(iZzhV);
    }
}
