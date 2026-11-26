package com.google.android.gms.internal;

import android.net.Uri;
import android.text.TextUtils;
import com.adobe.mobile.TargetLocationRequest;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import org.apache.http.cookie.SM;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaby {
    private String zzHD;
    private final zzaae zzMM;
    private List<String> zzRy;
    private String zzUS;
    private String zzUT;
    private List<String> zzUU;
    private String zzUV;
    private String zzUW;
    private String zzUX;
    private List<String> zzUY;
    private zzaee zzVk;
    private List<String> zzVl;
    private List<String> zzVm;
    private zzaak zzVo;
    private String zzVq;
    private List<String> zzVr;
    private boolean zzVs;
    private String zzVt;
    private zzaeq zzVu;
    private boolean zzVv;
    private long zzUZ = -1;
    private boolean zzVa = false;
    private final long zzVb = -1;
    private long zzVc = -1;
    private int mOrientation = -1;
    private boolean zzVd = false;
    private boolean zzVe = false;
    private boolean zzVf = false;
    private boolean zzVg = true;
    private boolean zzVh = true;
    private String zzVi = "";
    private boolean zzVj = false;
    private boolean zzMy = false;
    private boolean zzVn = false;
    private boolean zzVp = false;

    public zzaby(zzaae zzaaeVar, String str) {
        this.zzUT = str;
        this.zzMM = zzaaeVar;
    }

    private static String zza(Map<String, List<String>> map, String str) {
        List<String> list = map.get(str);
        if (list == null || list.isEmpty()) {
            return null;
        }
        return list.get(0);
    }

    private static long zzb(Map<String, List<String>> map, String str) {
        List<String> list = map.get(str);
        if (list != null && !list.isEmpty()) {
            String str2 = list.get(0);
            try {
                return (long) (Float.parseFloat(str2) * 1000.0f);
            } catch (NumberFormatException e) {
                zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 36 + String.valueOf(str2).length()).append("Could not parse float from ").append(str).append(" header: ").append(str2).toString());
            }
        }
        return -1L;
    }

    private static List<String> zzc(Map<String, List<String>> map, String str) {
        String str2;
        List<String> list = map.get(str);
        if (list == null || list.isEmpty() || (str2 = list.get(0)) == null) {
            return null;
        }
        return Arrays.asList(str2.trim().split("\\s+"));
    }

    private static boolean zzd(Map<String, List<String>> map, String str) {
        List<String> list = map.get(str);
        return (list == null || list.isEmpty() || !Boolean.valueOf(list.get(0)).booleanValue()) ? false : true;
    }

    public final void zza(String str, Map<String, List<String>> map, String str2) {
        this.zzHD = str2;
        zzh(map);
    }

    public final zzaai zze(long j) {
        return new zzaai(this.zzMM, this.zzUT, this.zzHD, this.zzUU, this.zzUY, this.zzUZ, this.zzVa, -1L, this.zzRy, this.zzVc, this.mOrientation, this.zzUS, j, this.zzUW, this.zzUX, this.zzVd, this.zzVe, this.zzVf, this.zzVg, false, this.zzVi, this.zzVj, this.zzMy, this.zzVk, this.zzVl, this.zzVm, this.zzVn, this.zzVo, this.zzVp, this.zzVq, this.zzVr, this.zzVs, this.zzVt, this.zzVu, this.zzUV, this.zzVh, this.zzVv);
    }

    public final void zzh(Map<String, List<String>> map) {
        this.zzUS = zza(map, "X-Afma-Ad-Size");
        this.zzVt = zza(map, "X-Afma-Ad-Slot-Size");
        List<String> listZzc = zzc(map, "X-Afma-Click-Tracking-Urls");
        if (listZzc != null) {
            this.zzUU = listZzc;
        }
        this.zzUV = zza(map, "X-Afma-Debug-Signals");
        List<String> list = map.get("X-Afma-Debug-Dialog");
        if (list != null && !list.isEmpty()) {
            this.zzUW = list.get(0);
        }
        List<String> listZzc2 = zzc(map, "X-Afma-Tracking-Urls");
        if (listZzc2 != null) {
            this.zzUY = listZzc2;
        }
        long jZzb = zzb(map, "X-Afma-Interstitial-Timeout");
        if (jZzb != -1) {
            this.zzUZ = jZzb;
        }
        this.zzVa |= zzd(map, "X-Afma-Mediation");
        List<String> listZzc3 = zzc(map, "X-Afma-Manual-Tracking-Urls");
        if (listZzc3 != null) {
            this.zzRy = listZzc3;
        }
        long jZzb2 = zzb(map, "X-Afma-Refresh-Rate");
        if (jZzb2 != -1) {
            this.zzVc = jZzb2;
        }
        List<String> list2 = map.get("X-Afma-Orientation");
        if (list2 != null && !list2.isEmpty()) {
            String str = list2.get(0);
            if ("portrait".equalsIgnoreCase(str)) {
                this.mOrientation = com.google.android.gms.ads.internal.zzbs.zzbB().zzhU();
            } else if ("landscape".equalsIgnoreCase(str)) {
                this.mOrientation = com.google.android.gms.ads.internal.zzbs.zzbB().zzhT();
            }
        }
        this.zzUX = zza(map, "X-Afma-ActiveView");
        List<String> list3 = map.get("X-Afma-Use-HTTPS");
        if (list3 != null && !list3.isEmpty()) {
            this.zzVf = Boolean.valueOf(list3.get(0)).booleanValue();
        }
        this.zzVd |= zzd(map, "X-Afma-Custom-Rendering-Allowed");
        this.zzVe = "native".equals(zza(map, "X-Afma-Ad-Format"));
        List<String> list4 = map.get("X-Afma-Content-Url-Opted-Out");
        if (list4 != null && !list4.isEmpty()) {
            this.zzVg = Boolean.valueOf(list4.get(0)).booleanValue();
        }
        List<String> list5 = map.get("X-Afma-Content-Vertical-Opted-Out");
        if (list5 != null && !list5.isEmpty()) {
            this.zzVh = Boolean.valueOf(list5.get(0)).booleanValue();
        }
        List<String> list6 = map.get("X-Afma-Gws-Query-Id");
        if (list6 != null && !list6.isEmpty()) {
            this.zzVi = list6.get(0);
        }
        String strZza = zza(map, "X-Afma-Fluid");
        if (strZza != null && strZza.equals("height")) {
            this.zzVj = true;
        }
        this.zzMy = "native_express".equals(zza(map, "X-Afma-Ad-Format"));
        this.zzVk = zzaee.zzaz(zza(map, "X-Afma-Rewards"));
        if (this.zzVl == null) {
            this.zzVl = zzc(map, "X-Afma-Reward-Video-Start-Urls");
        }
        if (this.zzVm == null) {
            this.zzVm = zzc(map, "X-Afma-Reward-Video-Complete-Urls");
        }
        this.zzVn |= zzd(map, "X-Afma-Use-Displayed-Impression");
        this.zzVp |= zzd(map, "X-Afma-Auto-Collect-Location");
        this.zzVq = zza(map, SM.SET_COOKIE);
        String strZza2 = zza(map, "X-Afma-Auto-Protection-Configuration");
        if (strZza2 == null || TextUtils.isEmpty(strZza2)) {
            Uri.Builder builderBuildUpon = Uri.parse("https://pagead2.googlesyndication.com/pagead/gen_204").buildUpon();
            builderBuildUpon.appendQueryParameter(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, "gmob-apps-blocked-navigation");
            if (!TextUtils.isEmpty(this.zzUW)) {
                builderBuildUpon.appendQueryParameter("debugDialog", this.zzUW);
            }
            boolean zBooleanValue = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCf)).booleanValue();
            String strValueOf = String.valueOf(builderBuildUpon.toString());
            String strValueOf2 = String.valueOf("navigationURL");
            this.zzVo = new zzaak(zBooleanValue, Arrays.asList(new StringBuilder(String.valueOf(strValueOf).length() + 18 + String.valueOf(strValueOf2).length()).append(strValueOf).append("&").append(strValueOf2).append("={NAVIGATION_URL}").toString()));
        } else {
            try {
                this.zzVo = zzaak.zze(new JSONObject(strZza2));
            } catch (JSONException e) {
                zzafr.zzc("Error parsing configuration JSON", e);
                this.zzVo = new zzaak();
            }
        }
        List<String> listZzc4 = zzc(map, "X-Afma-Remote-Ping-Urls");
        if (listZzc4 != null) {
            this.zzVr = listZzc4;
        }
        String strZza3 = zza(map, "X-Afma-Safe-Browsing");
        if (!TextUtils.isEmpty(strZza3)) {
            try {
                this.zzVu = zzaeq.zzf(new JSONObject(strZza3));
            } catch (JSONException e2) {
                zzafr.zzc("Error parsing safe browsing header", e2);
            }
        }
        this.zzVs |= zzd(map, "X-Afma-Render-In-Browser");
        String strZza4 = zza(map, "X-Afma-Pool");
        if (TextUtils.isEmpty(strZza4)) {
            return;
        }
        try {
            this.zzVv = new JSONObject(strZza4).getBoolean("never_pool");
        } catch (JSONException e3) {
            zzafr.zzc("Error parsing interstitial pool header", e3);
        }
    }
}
