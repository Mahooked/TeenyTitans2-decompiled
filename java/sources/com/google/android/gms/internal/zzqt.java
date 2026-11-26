package com.google.android.gms.internal;

import com.comscore.utils.Constants;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqt implements zzrd {
    zzqt() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        if (map.keySet().contains(Constants.DEFAULT_START_PAGE_NAME)) {
            zzakaVar.zzD(true);
        }
        if (map.keySet().contains("stop")) {
            zzakaVar.zzD(false);
        }
    }
}
