package com.google.android.gms.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.internal.zzbo;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzbcf implements com.google.android.gms.common.internal.zzj {
    private final boolean zzaCj;
    private final WeakReference<zzbcd> zzaDq;
    private final Api<?> zzayW;

    public zzbcf(zzbcd zzbcdVar, Api<?> api, boolean z) {
        this.zzaDq = new WeakReference<>(zzbcdVar);
        this.zzayW = api;
        this.zzaCj = z;
    }

    @Override // com.google.android.gms.common.internal.zzj
    public final void zzf(@NonNull ConnectionResult connectionResult) {
        zzbcd zzbcdVar = this.zzaDq.get();
        if (zzbcdVar == null) {
            return;
        }
        zzbo.zza(Looper.myLooper() == zzbcdVar.zzaCZ.zzaCl.getLooper(), "onReportServiceBinding must be called on the GoogleApiClient handler thread");
        zzbcdVar.zzaCv.lock();
        try {
            if (zzbcdVar.zzan(0)) {
                if (!connectionResult.isSuccess()) {
                    zzbcdVar.zzb(connectionResult, this.zzayW, this.zzaCj);
                }
                if (zzbcdVar.zzpW()) {
                    zzbcdVar.zzpX();
                }
            }
        } finally {
            zzbcdVar.zzaCv.unlock();
        }
    }
}
