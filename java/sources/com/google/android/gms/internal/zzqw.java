package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.text.TextUtils;
import com.adobe.mobile.TargetLocationRequest;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzqw implements zzrd {
    zzqw() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        PackageManager packageManager = zzakaVar.getContext().getPackageManager();
        try {
            try {
                JSONArray jSONArray = new JSONObject(map.get("data")).getJSONArray("intents");
                JSONObject jSONObject = new JSONObject();
                for (int i = 0; i < jSONArray.length(); i++) {
                    try {
                        JSONObject jSONObject2 = jSONArray.getJSONObject(i);
                        String strOptString = jSONObject2.optString(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID);
                        String strOptString2 = jSONObject2.optString("u");
                        String strOptString3 = jSONObject2.optString("i");
                        String strOptString4 = jSONObject2.optString("m");
                        String strOptString5 = jSONObject2.optString("p");
                        String strOptString6 = jSONObject2.optString("c");
                        jSONObject2.optString("f");
                        jSONObject2.optString("e");
                        Intent intent = new Intent();
                        if (!TextUtils.isEmpty(strOptString2)) {
                            intent.setData(Uri.parse(strOptString2));
                        }
                        if (!TextUtils.isEmpty(strOptString3)) {
                            intent.setAction(strOptString3);
                        }
                        if (!TextUtils.isEmpty(strOptString4)) {
                            intent.setType(strOptString4);
                        }
                        if (!TextUtils.isEmpty(strOptString5)) {
                            intent.setPackage(strOptString5);
                        }
                        if (!TextUtils.isEmpty(strOptString6)) {
                            String[] strArrSplit = strOptString6.split("/", 2);
                            if (strArrSplit.length == 2) {
                                intent.setComponent(new ComponentName(strArrSplit[0], strArrSplit[1]));
                            }
                        }
                        try {
                            jSONObject.put(strOptString, packageManager.resolveActivity(intent, 65536) != null);
                        } catch (JSONException e) {
                            zzafr.zzb("Error constructing openable urls response.", e);
                        }
                    } catch (JSONException e2) {
                        zzafr.zzb("Error parsing the intent data.", e2);
                    }
                }
                zzakaVar.zzb("openableIntents", jSONObject);
            } catch (JSONException e3) {
                zzakaVar.zzb("openableIntents", new JSONObject());
            }
        } catch (JSONException e4) {
            zzakaVar.zzb("openableIntents", new JSONObject());
        }
    }
}
