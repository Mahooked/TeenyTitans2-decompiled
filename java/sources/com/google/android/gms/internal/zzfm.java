package com.google.android.gms.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
public final class zzfm implements zzgs {
    private zzny zzxj;

    public zzfm(zzny zznyVar) {
        this.zzxj = zznyVar;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final View zzcv() {
        if (this.zzxj != null) {
            return this.zzxj.zzeu();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final boolean zzcw() {
        return this.zzxj == null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final zzgs zzcx() {
        return this;
    }
}
