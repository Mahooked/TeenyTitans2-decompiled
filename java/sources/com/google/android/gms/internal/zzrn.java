package com.google.android.gms.internal;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Future;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrn implements zzrd {
    private HashMap<String, zzajg<JSONObject>> zzJD = new HashMap<>();

    public final Future<JSONObject> zzS(String str) {
        zzajg<JSONObject> zzajgVar = new zzajg<>();
        this.zzJD.put(str, zzajgVar);
        return zzajgVar;
    }

    public final void zzT(String str) {
        zzajg<JSONObject> zzajgVar = this.zzJD.get(str);
        if (zzajgVar == null) {
            zzafr.e("Could not find the ad request for the corresponding ad response.");
            return;
        }
        if (!zzajgVar.isDone()) {
            zzajgVar.cancel(true);
        }
        this.zzJD.remove(str);
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("request_id");
        String str2 = map.get("fetched_ad");
        zzafr.zzaC("Received ad from the cache.");
        zzajg<JSONObject> zzajgVar = this.zzJD.get(str);
        if (zzajgVar == null) {
            zzafr.e("Could not find the ad request for the corresponding ad response.");
            return;
        }
        try {
            zzajgVar.zzg(new JSONObject(str2));
        } catch (JSONException e) {
            zzafr.zzb("Failed constructing JSON object from value passed from javascript", e);
            zzajgVar.zzg(null);
        } finally {
            this.zzJD.remove(str);
        }
    }
}
