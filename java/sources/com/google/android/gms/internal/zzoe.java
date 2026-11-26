package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzoe extends zzym {
    private /* synthetic */ JSONObject zzIg;

    zzoe(zzoc zzocVar, JSONObject jSONObject) {
        this.zzIg = jSONObject;
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzd(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("google.afma.nativeAds.handleImpressionPing", this.zzIg);
    }
}
