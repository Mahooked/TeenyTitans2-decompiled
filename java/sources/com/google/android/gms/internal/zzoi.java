package com.google.android.gms.internal;

import com.adobe.mobile.TargetLocationRequest;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzoi implements zzakf {
    private /* synthetic */ Map zzIl;
    private /* synthetic */ zzoh zzIm;

    zzoi(zzoh zzohVar, Map map) {
        this.zzIm = zzohVar;
        this.zzIl = map;
    }

    @Override // com.google.android.gms.internal.zzakf
    public final void zza(zzaka zzakaVar, boolean z) {
        this.zzIm.zzIk.zzIi = (String) this.zzIl.get(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID);
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("messageType", "htmlLoaded");
            jSONObject.put(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, this.zzIm.zzIk.zzIi);
            this.zzIm.zzIj.zzb("sendMessageToNativeJs", jSONObject);
        } catch (JSONException e) {
            zzafr.zzb("Unable to dispatch sendMessageToNativeJs event", e);
        }
    }
}
