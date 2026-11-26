package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzfx implements zzajs<com.google.android.gms.ads.internal.js.zzai> {
    private /* synthetic */ JSONObject zzxx;

    zzfx(zzfu zzfuVar, JSONObject jSONObject) {
        this.zzxx = jSONObject;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("AFMA_updateActiveView", this.zzxx);
    }
}
