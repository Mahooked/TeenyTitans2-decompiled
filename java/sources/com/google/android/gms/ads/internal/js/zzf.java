package com.google.android.gms.ads.internal.js;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzf implements Runnable {
    private /* synthetic */ String zzKU;
    private /* synthetic */ JSONObject zzKV;
    private /* synthetic */ zze zzKW;

    zzf(zze zzeVar, String str, JSONObject jSONObject) {
        this.zzKW = zzeVar;
        this.zzKU = str;
        this.zzKV = jSONObject;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzKW.zzJH.zza(this.zzKU, this.zzKV);
    }
}
