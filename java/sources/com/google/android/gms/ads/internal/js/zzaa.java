package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajs;

/* JADX INFO: loaded from: classes.dex */
final class zzaa implements zzajs<zzai> {
    private /* synthetic */ zzy zzLt;

    zzaa(zzy zzyVar) {
        this.zzLt = zzyVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(zzai zzaiVar) {
        zzafr.v("Releasing engine reference.");
        this.zzLt.zzLr.zzfb();
    }
}
