package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzaci implements zzajs<com.google.android.gms.ads.internal.js.zzai> {
    private /* synthetic */ zzabu zzUA;
    private /* synthetic */ zzacg zzWj;
    private /* synthetic */ zzrd zzWk;
    private /* synthetic */ JSONObject zzWl;

    zzaci(zzacg zzacgVar, zzrd zzrdVar, JSONObject jSONObject, zzabu zzabuVar) {
        this.zzWj = zzacgVar;
        this.zzWk = zzrdVar;
        this.zzWl = jSONObject;
        this.zzUA = zzabuVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        com.google.android.gms.ads.internal.js.zzai zzaiVar2 = zzaiVar;
        zzaiVar2.zza("/loadSdkConstants", this.zzWk);
        try {
            zzaiVar2.zza("AFMA_getSdkConstants", this.zzWl);
        } catch (Exception e) {
            zzafr.zzb("Error requesting an ad url", e);
            zzacg.zze(this.zzUA);
        }
    }
}
