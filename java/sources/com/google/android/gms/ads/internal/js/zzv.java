package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajq;

/* JADX INFO: loaded from: classes.dex */
final class zzv implements zzajq {
    private /* synthetic */ zzl zzLh;
    private /* synthetic */ zzac zzLo;

    zzv(zzl zzlVar, zzac zzacVar) {
        this.zzLh = zzlVar;
        this.zzLo = zzacVar;
    }

    @Override // com.google.android.gms.internal.zzajq
    public final void run() {
        synchronized (this.zzLh.mLock) {
            this.zzLh.zzLe = 1;
            zzafr.v("Failed loading new engine. Marking new engine destroyable.");
            this.zzLo.zzfc();
        }
    }
}
