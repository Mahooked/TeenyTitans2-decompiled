package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqy implements zzrd {
    zzqy() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = zzakaVar.zziu();
        if (zzmVarZziu != null) {
            zzmVarZziu.close();
            return;
        }
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziv = zzakaVar.zziv();
        if (zzmVarZziv != null) {
            zzmVarZziv.close();
        } else {
            zzafr.zzaT("A GMSG tried to close something that wasn't an overlay.");
        }
    }
}
