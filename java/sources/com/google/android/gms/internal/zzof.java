package com.google.android.gms.internal;

import android.text.TextUtils;
import java.lang.ref.WeakReference;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzof extends zzym implements zzrd {
    private final String zzIe;
    private final WeakReference<zznz> zztX;

    public zzof(zznz zznzVar, String str) {
        this.zztX = new WeakReference<>(zznzVar);
        this.zzIe = str;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        int i;
        zznz zznzVar;
        String str = map.get("ads_id");
        if (TextUtils.isEmpty(str) || !this.zzIe.equals(str)) {
            return;
        }
        try {
            i = Integer.parseInt(map.get("eventType"));
        } catch (Exception e) {
            zzafr.zzb("Parse Scion log event type error", e);
            i = -1;
        }
        if (1 == i) {
            zznz zznzVar2 = this.zztX.get();
            if (zznzVar2 != null) {
                zznzVar2.zzat();
                return;
            }
            return;
        }
        if (i != 0 || (zznzVar = this.zztX.get()) == null) {
            return;
        }
        zznzVar.zzau();
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzd(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("/logScionEvent", this);
    }
}
