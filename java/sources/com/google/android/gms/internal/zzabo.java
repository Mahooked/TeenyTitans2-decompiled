package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzabo implements zzajs<com.google.android.gms.ads.internal.js.zzai> {
    private /* synthetic */ zzmz zzUD;
    private /* synthetic */ zzabn zzUE;

    zzabo(zzabn zzabnVar, zzmz zzmzVar) {
        this.zzUE = zzabnVar;
        this.zzUD = zzmzVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        com.google.android.gms.ads.internal.js.zzai zzaiVar2 = zzaiVar;
        this.zzUE.zztd.zza(this.zzUD, "jsf");
        this.zzUE.zztd.zzdT();
        zzaiVar2.zza("/invalidRequest", this.zzUE.zzUA.zzUO);
        zzaiVar2.zza("/loadAdURL", this.zzUE.zzUA.zzUP);
        zzaiVar2.zza("/loadAd", this.zzUE.zzUA.zzUQ);
        try {
            zzaiVar2.zzi("AFMA_getAd", this.zzUE.zzUC);
        } catch (Exception e) {
            zzafr.zzb("Error requesting an ad url", e);
        }
    }
}
