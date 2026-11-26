package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzcsk extends zzcru {
    private /* synthetic */ zzcsa.zzd zzbBZ;

    zzcsk(zzcsa.zzd zzdVar) {
        this.zzbBZ = zzdVar;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zza(Status status, com.google.android.gms.safetynet.zzd zzdVar) {
        this.zzbBZ.setResult(new zzcsa.zzg(status, zzdVar));
    }
}
