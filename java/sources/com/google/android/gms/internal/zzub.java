package com.google.android.gms.internal;

import android.net.http.Headers;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzub {
    public final List<zzua> zzLY;
    public final long zzLZ;
    public final List<String> zzMa;
    public final List<String> zzMb;
    public final List<String> zzMc;
    public final List<String> zzMd;
    public final boolean zzMe;
    public final String zzMf;
    public final long zzMg;
    public final String zzMh;
    public final int zzMi;
    public final int zzMj;
    public final long zzMk;
    public final boolean zzMl;
    public final boolean zzMm;
    public int zzMn;
    public int zzMo;
    public boolean zzMp;

    public zzub(String str) throws JSONException {
        this(new JSONObject(str));
    }

    public zzub(List<zzua> list, long j, List<String> list2, List<String> list3, List<String> list4, List<String> list5, boolean z, String str, long j2, int i, int i2, String str2, int i3, int i4, long j3, boolean z2) {
        this.zzLY = list;
        this.zzLZ = j;
        this.zzMa = list2;
        this.zzMb = list3;
        this.zzMc = list4;
        this.zzMd = list5;
        this.zzMe = z;
        this.zzMf = str;
        this.zzMg = -1L;
        this.zzMn = 0;
        this.zzMo = 1;
        this.zzMh = null;
        this.zzMi = 0;
        this.zzMj = -1;
        this.zzMk = -1L;
        this.zzMl = false;
        this.zzMm = false;
        this.zzMp = false;
    }

    public zzub(JSONObject jSONObject) throws JSONException {
        if (zzafr.zzz(2)) {
            String strValueOf = String.valueOf(jSONObject.toString(2));
            zzafr.v(strValueOf.length() != 0 ? "Mediation Response JSON: ".concat(strValueOf) : new String("Mediation Response JSON: "));
        }
        JSONArray jSONArray = jSONObject.getJSONArray("ad_networks");
        ArrayList arrayList = new ArrayList(jSONArray.length());
        int i = -1;
        for (int i2 = 0; i2 < jSONArray.length(); i2++) {
            zzua zzuaVar = new zzua(jSONArray.getJSONObject(i2));
            if (zzuaVar.zzfh()) {
                this.zzMp = true;
            }
            arrayList.add(zzuaVar);
            if (i < 0 && zza(zzuaVar)) {
                i = i2;
            }
        }
        this.zzMn = i;
        this.zzMo = jSONArray.length();
        this.zzLY = Collections.unmodifiableList(arrayList);
        this.zzMf = jSONObject.optString("qdata");
        this.zzMj = jSONObject.optInt("fs_model_type", -1);
        this.zzMk = jSONObject.optLong("timeout_ms", -1L);
        JSONObject jSONObjectOptJSONObject = jSONObject.optJSONObject("settings");
        if (jSONObjectOptJSONObject == null) {
            this.zzLZ = -1L;
            this.zzMa = null;
            this.zzMb = null;
            this.zzMc = null;
            this.zzMd = null;
            this.zzMg = -1L;
            this.zzMh = null;
            this.zzMi = 0;
            this.zzMl = false;
            this.zzMe = false;
            this.zzMm = false;
            return;
        }
        this.zzLZ = jSONObjectOptJSONObject.optLong("ad_network_timeout_millis", -1L);
        com.google.android.gms.ads.internal.zzbs.zzbS();
        this.zzMa = zzuj.zza(jSONObjectOptJSONObject, "click_urls");
        com.google.android.gms.ads.internal.zzbs.zzbS();
        this.zzMb = zzuj.zza(jSONObjectOptJSONObject, "imp_urls");
        com.google.android.gms.ads.internal.zzbs.zzbS();
        this.zzMc = zzuj.zza(jSONObjectOptJSONObject, "nofill_urls");
        com.google.android.gms.ads.internal.zzbs.zzbS();
        this.zzMd = zzuj.zza(jSONObjectOptJSONObject, "remote_ping_urls");
        this.zzMe = jSONObjectOptJSONObject.optBoolean("render_in_browser", false);
        long jOptLong = jSONObjectOptJSONObject.optLong(Headers.REFRESH, -1L);
        this.zzMg = jOptLong > 0 ? jOptLong * 1000 : -1L;
        zzaee zzaeeVarZza = zzaee.zza(jSONObjectOptJSONObject.optJSONArray("rewards"));
        if (zzaeeVarZza == null) {
            this.zzMh = null;
            this.zzMi = 0;
        } else {
            this.zzMh = zzaeeVarZza.type;
            this.zzMi = zzaeeVarZza.zzWW;
        }
        this.zzMl = jSONObjectOptJSONObject.optBoolean("use_displayed_impression", false);
        this.zzMm = jSONObjectOptJSONObject.optBoolean("allow_pub_rendered_attribution", false);
    }

    private static boolean zza(zzua zzuaVar) {
        Iterator<String> it = zzuaVar.zzLJ.iterator();
        while (it.hasNext()) {
            if (it.next().equals("com.google.ads.mediation.admob.AdMobAdapter")) {
                return true;
            }
        }
        return false;
    }
}
