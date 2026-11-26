package com.google.android.gms.internal;

import android.view.View;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
public final class zzfo implements zzgs {
    private final WeakReference<View> zzxl;
    private final WeakReference<zzaff> zzxm;

    public zzfo(View view, zzaff zzaffVar) {
        this.zzxl = new WeakReference<>(view);
        this.zzxm = new WeakReference<>(zzaffVar);
    }

    @Override // com.google.android.gms.internal.zzgs
    public final View zzcv() {
        return this.zzxl.get();
    }

    @Override // com.google.android.gms.internal.zzgs
    public final boolean zzcw() {
        return this.zzxl.get() == null || this.zzxm.get() == null;
    }

    @Override // com.google.android.gms.internal.zzgs
    public final zzgs zzcx() {
        return new zzfn(this.zzxl.get(), this.zzxm.get());
    }
}
