package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqq implements zzrd {
    zzqq() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEB)).booleanValue()) {
            zzakaVar.zzC(!Boolean.parseBoolean(map.get("disabled")));
        }
    }
}
