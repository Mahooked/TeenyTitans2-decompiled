package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzyr implements zzrd {
    private /* synthetic */ zzyn zzRF;
    private /* synthetic */ zznu zzRJ;

    zzyr(zzyn zzynVar, zznu zznuVar) {
        this.zzRF = zzynVar;
        this.zzRJ = zznuVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        this.zzRF.zzc(this.zzRJ, map.get("asset"));
    }
}
