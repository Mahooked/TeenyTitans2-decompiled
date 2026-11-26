package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzru implements zzrd {
    private final zzrv zzJJ;

    public zzru(zzrv zzrvVar) {
        this.zzJJ = zzrvVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzaee zzaeeVar;
        String str = map.get("action");
        if (!"grant".equals(str)) {
            if ("video_start".equals(str)) {
                this.zzJJ.zzbc();
            }
        } else {
            try {
                int i = Integer.parseInt(map.get("amount"));
                String str2 = map.get("type");
                zzaeeVar = !TextUtils.isEmpty(str2) ? new zzaee(str2, i) : null;
            } catch (NumberFormatException e) {
                zzafr.zzc("Unable to parse reward amount.", e);
            }
            this.zzJJ.zzb(zzaeeVar);
        }
    }
}
