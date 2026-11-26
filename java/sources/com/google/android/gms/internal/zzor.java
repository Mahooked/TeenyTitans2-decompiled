package com.google.android.gms.internal;

import android.view.MotionEvent;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzor implements zznw {
    private /* synthetic */ zzop zzIB;
    private /* synthetic */ View zzIC;

    zzor(zzop zzopVar, View view) {
        this.zzIB = zzopVar;
        this.zzIC = view;
    }

    @Override // com.google.android.gms.internal.zznw
    public final void zzc(MotionEvent motionEvent) {
        this.zzIB.onTouch(null, motionEvent);
    }

    @Override // com.google.android.gms.internal.zznw
    public final void zzeo() {
        this.zzIB.onClick(this.zzIC);
    }
}
