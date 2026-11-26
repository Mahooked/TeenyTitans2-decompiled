package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzoj implements zzrd {
    private /* synthetic */ com.google.android.gms.ads.internal.js.zzai zzIj;
    private /* synthetic */ zzog zzIk;

    zzoj(zzog zzogVar, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        this.zzIk = zzogVar;
        this.zzIj = zzaiVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzaka zzakaVar2 = (zzaka) this.zzIk.zzIh.get();
        if (zzakaVar2 == null) {
            this.zzIj.zzb("/showOverlay", this);
        } else {
            zzakaVar2.getView().setVisibility(0);
        }
    }
}
