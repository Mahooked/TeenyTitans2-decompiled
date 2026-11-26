package com.google.android.gms.internal;

import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzog {
    private final WeakReference<zzaka> zzIh;
    private String zzIi;

    public zzog(zzaka zzakaVar) {
        this.zzIh = new WeakReference<>(zzakaVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final zzrd zze(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        return new zzoh(this, zzaiVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final zzrd zzf(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        return new zzoj(this, zzaiVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final zzrd zzg(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        return new zzok(this, zzaiVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final zzrd zzh(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        return new zzol(this, zzaiVar);
    }
}
