package com.google.android.gms.ads.internal.overlay;

import android.graphics.drawable.Drawable;

/* JADX INFO: loaded from: classes.dex */
final class zzt implements Runnable {
    private /* synthetic */ Drawable zzPb;
    private /* synthetic */ zzs zzPc;

    zzt(zzs zzsVar, Drawable drawable) {
        this.zzPc = zzsVar;
        this.zzPb = drawable;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzPc.zzOY.mActivity.getWindow().setBackgroundDrawable(this.zzPb);
    }
}
