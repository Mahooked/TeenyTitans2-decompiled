package com.google.android.gms.internal;

import android.support.annotation.BinderThread;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzbck extends zzctp {
    private final WeakReference<zzbcd> zzaDq;

    zzbck(zzbcd zzbcdVar) {
        this.zzaDq = new WeakReference<>(zzbcdVar);
    }

    @Override // com.google.android.gms.internal.zzctp, com.google.android.gms.internal.zzctq
    @BinderThread
    public final void zzb(zzctx zzctxVar) {
        zzbcd zzbcdVar = this.zzaDq.get();
        if (zzbcdVar == null) {
            return;
        }
        zzbcdVar.zzaCZ.zza(new zzbcl(this, zzbcdVar, zzbcdVar, zzctxVar));
    }
}
