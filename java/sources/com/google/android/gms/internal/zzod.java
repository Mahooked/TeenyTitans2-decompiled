package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzod extends zzym {
    private /* synthetic */ JSONObject zzIg;

    zzod(zzoc zzocVar, JSONObject jSONObject) {
        this.zzIg = jSONObject;
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzd(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("google.afma.nativeAds.handleClickGmsg", this.zzIg);
    }
}
