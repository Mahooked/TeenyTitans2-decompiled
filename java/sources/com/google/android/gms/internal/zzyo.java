package com.google.android.gms.internal;

import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzyo extends zzym {
    final /* synthetic */ String zzRC;
    final /* synthetic */ zzyw zzRD;
    final /* synthetic */ zzajg zzRE;
    final /* synthetic */ zzyn zzRF;

    zzyo(zzyn zzynVar, String str, zzyw zzywVar, zzajg zzajgVar) {
        this.zzRF = zzynVar;
        this.zzRC = str;
        this.zzRD = zzywVar;
        this.zzRE = zzajgVar;
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzd(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzyp zzypVar = new zzyp(this, zzaiVar);
        this.zzRD.zzRW = zzypVar;
        zzaiVar.zza("/nativeAdPreProcess", zzypVar);
        try {
            JSONObject jSONObject = new JSONObject(this.zzRF.zzQQ.zzXY.body);
            jSONObject.put("ads_id", this.zzRC);
            zzaiVar.zza("google.afma.nativeAds.preProcessJsonGmsg", jSONObject);
        } catch (JSONException e) {
            zzafr.zzc("Exception occurred while invoking javascript", e);
            this.zzRE.zzg(null);
        }
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzgv() {
        this.zzRE.zzg(null);
    }
}
