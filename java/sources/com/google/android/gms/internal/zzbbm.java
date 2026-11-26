package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzbbm implements zzbdq {
    private /* synthetic */ zzbbk zzaCx;

    private zzbbm(zzbbk zzbbkVar) {
        this.zzaCx = zzbbkVar;
    }

    /* synthetic */ zzbbm(zzbbk zzbbkVar, zzbbl zzbblVar) {
        this(zzbbkVar);
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zzc(@NonNull ConnectionResult connectionResult) {
        this.zzaCx.zzaCv.lock();
        try {
            this.zzaCx.zzaCs = connectionResult;
            this.zzaCx.zzpF();
        } finally {
            this.zzaCx.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zze(int i, boolean z) {
        this.zzaCx.zzaCv.lock();
        try {
            if (this.zzaCx.zzaCu || this.zzaCx.zzaCt == null || !this.zzaCx.zzaCt.isSuccess()) {
                this.zzaCx.zzaCu = false;
                this.zzaCx.zzd(i, z);
            } else {
                this.zzaCx.zzaCu = true;
                this.zzaCx.zzaCn.onConnectionSuspended(i);
            }
        } finally {
            this.zzaCx.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zzm(@Nullable Bundle bundle) {
        this.zzaCx.zzaCv.lock();
        try {
            this.zzaCx.zzl(bundle);
            this.zzaCx.zzaCs = ConnectionResult.zzazX;
            this.zzaCx.zzpF();
        } finally {
            this.zzaCx.zzaCv.unlock();
        }
    }
}
