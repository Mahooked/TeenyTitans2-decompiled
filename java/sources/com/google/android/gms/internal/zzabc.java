package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzabc implements zzajs<com.google.android.gms.ads.internal.js.zzai> {
    private /* synthetic */ zzabb zzUf;

    zzabc(zzabb zzabbVar) {
        this.zzUf = zzabbVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        try {
            zzaiVar.zza("AFMA_getAdapterLessMediationAd", this.zzUf.zzUd);
        } catch (Exception e) {
            zzafr.zzb("Error requesting an ad url", e);
            zzaaz.zzTZ.zzT(this.zzUf.zzUe);
        }
    }
}
