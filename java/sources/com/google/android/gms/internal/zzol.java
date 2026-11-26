package com.google.android.gms.internal;

import com.adobe.mobile.TargetLocationRequest;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzol implements zzrd {
    private /* synthetic */ com.google.android.gms.ads.internal.js.zzai zzIj;
    private /* synthetic */ zzog zzIk;

    zzol(zzog zzogVar, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        this.zzIk = zzogVar;
        this.zzIj = zzaiVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        JSONObject jSONObject = new JSONObject();
        try {
            for (String str : map.keySet()) {
                jSONObject.put(str, map.get(str));
            }
            jSONObject.put(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, this.zzIk.zzIi);
            this.zzIj.zzb("sendMessageToNativeJs", jSONObject);
        } catch (JSONException e) {
            zzafr.zzb("Unable to dispatch sendMessageToNativeJs event", e);
        }
    }
}
