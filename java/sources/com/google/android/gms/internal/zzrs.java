package com.google.android.gms.internal;

import android.text.TextUtils;
import com.adobe.mobile.TargetLocationRequest;
import java.util.HashMap;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrs implements zzrd {
    private final Object mLock = new Object();
    private final Map<String, zzrt> zzJI = new HashMap();

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID);
        String str2 = map.get("fail");
        map.get("fail_reason");
        String str3 = map.get("result");
        synchronized (this.mLock) {
            if (this.zzJI.remove(str) == null) {
                String strValueOf = String.valueOf(str);
                zzafr.zzaT(strValueOf.length() != 0 ? "Received result for unexpected method invocation: ".concat(strValueOf) : new String("Received result for unexpected method invocation: "));
            } else if (TextUtils.isEmpty(str2)) {
                if (str3 == null) {
                    return;
                }
                try {
                    new JSONObject(str3);
                } catch (JSONException e) {
                    e.getMessage();
                }
            }
        }
    }
}
