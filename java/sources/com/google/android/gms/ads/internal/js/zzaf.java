package com.google.android.gms.ads.internal.js;

import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzajs;

/* JADX INFO: loaded from: classes.dex */
final class zzaf implements zzajs<zza> {
    final /* synthetic */ zzac zzLx;

    zzaf(zzac zzacVar) {
        this.zzLx = zzacVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(zza zzaVar) {
        zzbs.zzbz();
        zzagz.runOnUiThread(new zzag(this, zzaVar));
    }
}
