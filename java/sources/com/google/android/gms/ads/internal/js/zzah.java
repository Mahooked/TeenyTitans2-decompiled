package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzajq;
import com.google.android.gms.internal.zzajs;
import com.google.android.gms.internal.zzajt;

/* JADX INFO: loaded from: classes.dex */
public final class zzah extends zzajt<zzai> {
    private zzy zzLA;

    public zzah(zzy zzyVar) {
        this.zzLA = zzyVar;
    }

    public final void finalize() {
        this.zzLA.release();
        this.zzLA = null;
    }

    @Override // com.google.android.gms.internal.zzajt
    public final int getStatus() {
        return this.zzLA.getStatus();
    }

    @Override // com.google.android.gms.internal.zzajt
    public final void reject() {
        this.zzLA.reject();
    }

    @Override // com.google.android.gms.internal.zzajt, com.google.android.gms.internal.zzajp
    public final void zza(zzajs<zzai> zzajsVar, zzajq zzajqVar) {
        this.zzLA.zza(zzajsVar, zzajqVar);
    }

    @Override // com.google.android.gms.internal.zzajt, com.google.android.gms.internal.zzajp
    public final /* synthetic */ void zzf(Object obj) {
        this.zzLA.zzf((zzai) obj);
    }
}
