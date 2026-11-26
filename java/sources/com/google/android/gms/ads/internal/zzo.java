package com.google.android.gms.ads.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzo implements View.OnClickListener {
    private /* synthetic */ zzl zzte;
    private /* synthetic */ zzw zztf;

    zzo(zzl zzlVar, zzw zzwVar) {
        this.zzte = zzlVar;
        this.zztf = zzwVar;
    }

    @Override // android.view.View.OnClickListener
    public final void onClick(View view) {
        this.zztf.recordClick();
        if (this.zzte.zztc != null) {
            this.zzte.zztc.zzha();
        }
    }
}
