package com.google.android.gms.internal;

import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
public final class zzdq implements Callable {
    private final zzdb zzpJ;
    private final zzax zzro;

    public zzdq(zzdb zzdbVar, zzax zzaxVar) {
        this.zzpJ = zzdbVar;
        this.zzro = zzaxVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // java.util.concurrent.Callable
    /* JADX INFO: renamed from: zzV, reason: merged with bridge method [inline-methods] */
    public final Void call() throws Exception {
        if (this.zzpJ.zzL() != null) {
            this.zzpJ.zzL().get();
        }
        zzax zzaxVarZzK = this.zzpJ.zzK();
        if (zzaxVarZzK == null) {
            return null;
        }
        try {
            synchronized (this.zzro) {
                adp.zza(this.zzro, adp.zzc(zzaxVarZzK));
            }
            return null;
        } catch (ado e) {
            return null;
        }
    }
}
