package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.support.annotation.BinderThread;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
public final class zzo extends zze {
    private /* synthetic */ zzd zzaHe;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    @BinderThread
    public zzo(zzd zzdVar, @Nullable int i, Bundle bundle) {
        super(zzdVar, i, null);
        this.zzaHe = zzdVar;
    }

    @Override // com.google.android.gms.common.internal.zze
    protected final void zzj(ConnectionResult connectionResult) {
        this.zzaHe.zzaGQ.zzf(connectionResult);
        this.zzaHe.onConnectionFailed(connectionResult);
    }

    @Override // com.google.android.gms.common.internal.zze
    protected final boolean zzrj() {
        this.zzaHe.zzaGQ.zzf(ConnectionResult.zzazX);
        return true;
    }
}
