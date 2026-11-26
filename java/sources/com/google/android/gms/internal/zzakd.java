package com.google.android.gms.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzakd implements View.OnAttachStateChangeListener {
    private /* synthetic */ zzaet zzabH;
    private /* synthetic */ zzakb zzabJ;

    zzakd(zzakb zzakbVar, zzaet zzaetVar) {
        this.zzabJ = zzakbVar;
        this.zzabH = zzaetVar;
    }

    @Override // android.view.View.OnAttachStateChangeListener
    public final void onViewAttachedToWindow(View view) {
        this.zzabJ.zza(view, this.zzabH, 10);
    }

    @Override // android.view.View.OnAttachStateChangeListener
    public final void onViewDetachedFromWindow(View view) {
    }
}
