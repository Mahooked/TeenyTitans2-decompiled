package com.google.android.gms.internal;

import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbo;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzyp implements zzrd {
    private /* synthetic */ com.google.android.gms.ads.internal.js.zzai zzIj;
    private /* synthetic */ zzyo zzRG;

    zzyp(zzyo zzyoVar, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        this.zzRG = zzyoVar;
        this.zzIj = zzaiVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        JSONObject jSONObject;
        boolean z;
        try {
            String str = map.get("success");
            String str2 = map.get("failure");
            if (TextUtils.isEmpty(str2)) {
                jSONObject = new JSONObject(str);
                z = true;
            } else {
                jSONObject = new JSONObject(str2);
                z = false;
            }
            if (this.zzRG.zzRC.equals(jSONObject.optString("ads_id", ""))) {
                this.zzIj.zzb("/nativeAdPreProcess", this.zzRG.zzRD.zzRW);
                if (z) {
                    JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("ads");
                    if (jSONArrayOptJSONArray != null && jSONArrayOptJSONArray.length() > 0) {
                        this.zzRG.zzRE.zzg(jSONArrayOptJSONArray.getJSONObject(0));
                        return;
                    } else {
                        this.zzRG.zzRF.zzt(3);
                        this.zzRG.zzRE.zzg(null);
                        return;
                    }
                }
                this.zzRG.zzRF.zzt(0);
                zzbo.zza(this.zzRG.zzRF.zzgx(), "Unable to set the ad state error!");
                this.zzRG.zzRE.zzg(null);
            }
        } catch (JSONException e) {
            zzafr.zzb("Malformed native JSON response.", e);
        }
    }
}
