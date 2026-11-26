package com.google.android.gms.internal;

import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaah extends zzaaq {
    private final WeakReference<zzzw> zzTm;

    public zzaah(zzzw zzzwVar) {
        this.zzTm = new WeakReference<>(zzzwVar);
    }

    @Override // com.google.android.gms.internal.zzaap
    public final void zza(zzaai zzaaiVar) {
        zzzw zzzwVar = this.zzTm.get();
        if (zzzwVar != null) {
            zzzwVar.zza(zzaaiVar);
        }
    }
}
