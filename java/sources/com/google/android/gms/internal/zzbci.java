package com.google.android.gms.internal;

import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzbci extends zzbcy {
    private /* synthetic */ com.google.android.gms.common.internal.zzj zzaDu;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzbci(zzbcg zzbcgVar, zzbcw zzbcwVar, com.google.android.gms.common.internal.zzj zzjVar) {
        super(zzbcwVar);
        this.zzaDu = zzjVar;
    }

    @Override // com.google.android.gms.internal.zzbcy
    public final void zzpV() {
        this.zzaDu.zzf(new ConnectionResult(16, null));
    }
}
