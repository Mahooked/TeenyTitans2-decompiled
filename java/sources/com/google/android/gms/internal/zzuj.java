package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzuj {
    private static String zza(String str, String str2, String str3) {
        if (TextUtils.isEmpty(str3)) {
            str3 = "";
        }
        return str.replaceAll(str2, str3);
    }

    public static List<String> zza(JSONObject jSONObject, String str) throws JSONException {
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray(str);
        if (jSONArrayOptJSONArray == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList(jSONArrayOptJSONArray.length());
        for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
            arrayList.add(jSONArrayOptJSONArray.getString(i));
        }
        return Collections.unmodifiableList(arrayList);
    }

    public static void zza(Context context, String str, zzaff zzaffVar, String str2, boolean z, List<String> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        String str3 = z ? "1" : "0";
        Iterator<String> it = list.iterator();
        while (it.hasNext()) {
            String strZza = zza(zza(zza(zza(zza(zza(zza(it.next(), "@gw_adlocid@", str2), "@gw_adnetrefresh@", str3), "@gw_qdata@", zzaffVar.zzXN.zzMf), "@gw_sdkver@", str), "@gw_sessid@", com.google.android.gms.ads.internal.zzbs.zzbD().getSessionId()), "@gw_seqnum@", zzaffVar.zzSC), "@gw_adnetstatus@", zzaffVar.zzXO);
            if (zzaffVar.zzMG != null) {
                strZza = zza(zza(strZza, "@gw_adnetid@", zzaffVar.zzMG.zzLI), "@gw_allocid@", zzaffVar.zzMG.zzLK);
            }
            String strZzb = zzaez.zzb(strZza, context);
            com.google.android.gms.ads.internal.zzbs.zzbz();
            zzagz.zzd(context, str, strZzb);
        }
    }

    public static boolean zza(String str, int[] iArr) {
        if (TextUtils.isEmpty(str) || iArr.length != 2) {
            return false;
        }
        String[] strArrSplit = str.split("x");
        if (strArrSplit.length != 2) {
            return false;
        }
        try {
            iArr[0] = Integer.parseInt(strArrSplit[0]);
            iArr[1] = Integer.parseInt(strArrSplit[1]);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }
}
