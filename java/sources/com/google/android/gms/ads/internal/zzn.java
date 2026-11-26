package com.google.android.gms.ads.internal;

import android.view.MotionEvent;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzn implements View.OnTouchListener {
    private /* synthetic */ zzl zzte;
    private /* synthetic */ zzw zztf;

    zzn(zzl zzlVar, zzw zzwVar) {
        this.zzte = zzlVar;
        this.zztf = zzwVar;
    }

    @Override // android.view.View.OnTouchListener
    public final boolean onTouch(View view, MotionEvent motionEvent) {
        this.zztf.recordClick();
        if (this.zzte.zztc == null) {
            return false;
        }
        this.zzte.zztc.zzha();
        return false;
    }
}
