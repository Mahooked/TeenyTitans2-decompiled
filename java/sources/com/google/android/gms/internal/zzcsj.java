package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzcsj extends zzcru {
    private /* synthetic */ zzcsa.zzc zzbBY;

    zzcsj(zzcsa.zzc zzcVar) {
        this.zzbBY = zzcVar;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zza(Status status, boolean z) {
        this.zzbBY.setResult(new zzcsa.zzj(status, z));
    }
}
