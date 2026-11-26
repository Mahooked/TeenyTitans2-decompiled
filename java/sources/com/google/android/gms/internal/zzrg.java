package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzrg implements Runnable {
    private /* synthetic */ JSONObject zzJr;
    private /* synthetic */ zzrf zzJs;

    zzrg(zzrf zzrfVar, JSONObject jSONObject) {
        this.zzJs = zzrfVar;
        this.zzJr = jSONObject;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzJs.zzJp.zzb("fetchHttpRequestCompleted", this.zzJr);
        zzafr.zzaC("Dispatched http response.");
    }
}
