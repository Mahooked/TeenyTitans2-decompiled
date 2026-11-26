package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafj {
    private String mAppId;
    private final long zzYk;
    private String zzYo;
    private final List<String> zzYl = new ArrayList();
    private final List<String> zzYm = new ArrayList();
    private final Map<String, zzub> zzYn = new HashMap();
    private boolean zzYp = false;

    public zzafj(String str, long j) {
        this.zzYo = str;
        this.zzYk = j;
        zzaE(str);
    }

    private final void zzaE(String str) {
        JSONObject jSONObjectOptJSONObject;
        if (TextUtils.isEmpty(str)) {
            return;
        }
        try {
            JSONObject jSONObject = new JSONObject(str);
            if (jSONObject.optInt("status", -1) != 1) {
                this.zzYp = false;
                zzafr.zzaT("App settings could not be fetched successfully.");
                return;
            }
            this.zzYp = true;
            this.mAppId = jSONObject.optString("app_id");
            JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("ad_unit_id_settings");
            if (jSONArrayOptJSONArray != null) {
                for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
                    JSONObject jSONObject2 = jSONArrayOptJSONArray.getJSONObject(i);
                    String strOptString = jSONObject2.optString("format");
                    String strOptString2 = jSONObject2.optString("ad_unit_id");
                    if (!TextUtils.isEmpty(strOptString) && !TextUtils.isEmpty(strOptString2)) {
                        if ("interstitial".equalsIgnoreCase(strOptString)) {
                            this.zzYm.add(strOptString2);
                        } else if ("rewarded".equalsIgnoreCase(strOptString) && (jSONObjectOptJSONObject = jSONObject2.optJSONObject("mediation_config")) != null) {
                            this.zzYn.put(strOptString2, new zzub(jSONObjectOptJSONObject));
                        }
                    }
                }
            }
            zzg(jSONObject);
        } catch (JSONException e) {
            zzafr.zzc("Exception occurred while processing app setting json", e);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "AppSettings.parseAppSettingsJson");
        }
    }

    private final void zzg(JSONObject jSONObject) {
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("persistable_banner_ad_unit_ids");
        if (jSONArrayOptJSONArray != null) {
            for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
                this.zzYl.add(jSONArrayOptJSONArray.optString(i));
            }
        }
    }

    public final long zzhi() {
        return this.zzYk;
    }

    public final boolean zzhj() {
        return this.zzYp;
    }

    public final String zzhk() {
        return this.zzYo;
    }

    public final String zzhl() {
        return this.mAppId;
    }

    public final Map<String, zzub> zzhm() {
        return this.zzYn;
    }
}
