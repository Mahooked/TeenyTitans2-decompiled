package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzagz;

/* JADX INFO: loaded from: classes.dex */
final class zzn implements zzb {
    final /* synthetic */ zza zzLi;
    final /* synthetic */ zzm zzLj;

    zzn(zzm zzmVar, zza zzaVar) {
        this.zzLj = zzmVar;
        this.zzLi = zzaVar;
    }

    @Override // com.google.android.gms.ads.internal.js.zzb
    public final void zzeZ() {
        zzagz.zzZr.postDelayed(new zzo(this), zzw.zzLq);
    }
}
