package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajq;

/* JADX INFO: loaded from: classes.dex */
final class zzae implements zzajq {
    private /* synthetic */ zzy zzLw;

    zzae(zzac zzacVar, zzy zzyVar) {
        this.zzLw = zzyVar;
    }

    @Override // com.google.android.gms.internal.zzajq
    public final void run() {
        zzafr.v("Rejecting reference for JS Engine.");
        this.zzLw.reject();
    }
}
