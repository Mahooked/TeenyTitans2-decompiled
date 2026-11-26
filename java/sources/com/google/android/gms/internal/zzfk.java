package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.view.View;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
public final class zzfk implements zzgs {
    private WeakReference<zzny> zzxi;

    public zzfk(zzny zznyVar) {
        this.zzxi = new WeakReference<>(zznyVar);
    }

    @Override // com.google.android.gms.internal.zzgs
    @Nullable
    public final View zzcv() {
        zzny zznyVar = this.zzxi.get();
        if (zznyVar != null) {
            return zznyVar.zzeu();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final boolean zzcw() {
        return this.zzxi.get() == null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final zzgs zzcx() {
        return new zzfm(this.zzxi.get());
    }
}
