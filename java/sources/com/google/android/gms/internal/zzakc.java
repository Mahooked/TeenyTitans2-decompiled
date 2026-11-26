package com.google.android.gms.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzakc implements Runnable {
    private /* synthetic */ View val$view;
    private /* synthetic */ zzaet zzabH;
    private /* synthetic */ int zzabI;
    private /* synthetic */ zzakb zzabJ;

    zzakc(zzakb zzakbVar, View view, zzaet zzaetVar, int i) {
        this.zzabJ = zzakbVar;
        this.val$view = view;
        this.zzabH = zzaetVar;
        this.zzabI = i;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzabJ.zza(this.val$view, this.zzabH, this.zzabI - 1);
    }
}
