package com.google.android.gms.internal;

import java.util.Map;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzrf implements Runnable {
    private /* synthetic */ Map zzIl;
    final /* synthetic */ zzaka zzJp;
    private /* synthetic */ zzre zzJq;

    zzrf(zzre zzreVar, Map map, zzaka zzakaVar) {
        this.zzJq = zzreVar;
        this.zzIl = map;
        this.zzJp = zzakaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzafr.zzaC("Received Http request.");
        JSONObject jSONObjectZzR = this.zzJq.zzR((String) this.zzIl.get("http_request"));
        if (jSONObjectZzR == null) {
            zzafr.e("Response should not be null.");
        } else {
            zzagz.zzZr.post(new zzrg(this, jSONObjectZzR));
        }
    }
}
