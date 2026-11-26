package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
public final class zzfn implements zzgs {

    @Nullable
    private final View mView;

    @Nullable
    private final zzaff zzxk;

    public zzfn(View view, zzaff zzaffVar) {
        this.mView = view;
        this.zzxk = zzaffVar;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final View zzcv() {
        return this.mView;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final boolean zzcw() {
        return this.zzxk == null || this.mView == null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final zzgs zzcx() {
        return this;
    }
}
