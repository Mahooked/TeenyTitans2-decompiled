package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzcsl extends zzcru {
    private /* synthetic */ zzcsa.zze zzbCa;

    zzcsl(zzcsa.zze zzeVar) {
        this.zzbCa = zzeVar;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zza(Status status, com.google.android.gms.safetynet.zzf zzfVar) {
        this.zzbCa.setResult(new zzcsa.zzh(status, zzfVar));
    }
}
