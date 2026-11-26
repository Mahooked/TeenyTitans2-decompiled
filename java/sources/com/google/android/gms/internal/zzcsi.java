package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzcsi extends zzcru {
    private /* synthetic */ zzcsa.zzb zzbBX;

    zzcsi(zzcsa.zzb zzbVar) {
        this.zzbBX = zzbVar;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zza(Status status, com.google.android.gms.safetynet.zza zzaVar) {
        this.zzbBX.setResult(new zzcsa.zza(status, zzaVar));
    }
}
