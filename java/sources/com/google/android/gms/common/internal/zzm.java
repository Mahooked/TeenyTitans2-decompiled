package com.google.android.gms.common.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
public final class zzm implements zzj {
    private /* synthetic */ zzd zzaHe;

    public zzm(zzd zzdVar) {
        this.zzaHe = zzdVar;
    }

    @Override // com.google.android.gms.common.internal.zzj
    public final void zzf(@NonNull ConnectionResult connectionResult) {
        if (connectionResult.isSuccess()) {
            this.zzaHe.zza((zzal) null, this.zzaHe.zzrh());
        } else if (this.zzaHe.zzaGW != null) {
            this.zzaHe.zzaGW.onConnectionFailed(connectionResult);
        }
    }
}
