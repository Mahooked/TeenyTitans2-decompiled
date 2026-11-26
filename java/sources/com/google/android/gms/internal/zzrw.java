package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Color;
import android.os.Build;
import android.os.SystemClock;
import android.text.TextUtils;
import android.view.MotionEvent;
import com.comscore.utils.Constants;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import java.util.HashMap;
import java.util.Map;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrw implements zzrd {
    private boolean zzJK;

    private static int zza(Context context, Map<String, String> map, String str, int i) {
        String str2 = map.get(str);
        if (str2 == null) {
            return i;
        }
        try {
            zzji.zzds();
            return zzaiy.zzc(context, Integer.parseInt(str2));
        } catch (NumberFormatException e) {
            zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 34 + String.valueOf(str2).length()).append("Could not parse ").append(str).append(" in a video GMSG: ").append(str2).toString());
            return i;
        }
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        int iMin;
        int i;
        String str = map.get("action");
        if (str == null) {
            zzafr.zzaT("Action missing from video GMSG.");
            return;
        }
        if (zzafr.zzz(3)) {
            JSONObject jSONObject = new JSONObject(map);
            jSONObject.remove("google.afma.Notify_dt");
            String strValueOf = String.valueOf(jSONObject.toString());
            zzafr.zzaC(new StringBuilder(String.valueOf(str).length() + 13 + String.valueOf(strValueOf).length()).append("Video GMSG: ").append(str).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf).toString());
        }
        if (Constants.DEFAULT_BACKGROUND_PAGE_NAME.equals(str)) {
            String str2 = map.get("color");
            if (TextUtils.isEmpty(str2)) {
                zzafr.zzaT("Color parameter missing from color video GMSG.");
                return;
            }
            try {
                zzakaVar.setBackgroundColor(Color.parseColor(str2));
                return;
            } catch (IllegalArgumentException e) {
                zzafr.zzaT("Invalid color parameter in video GMSG.");
                return;
            }
        }
        if ("decoderProps".equals(str)) {
            String str3 = map.get("mimeTypes");
            if (str3 == null) {
                zzafr.zzaT("No MIME types specified for decoder properties inspection.");
                com.google.android.gms.ads.internal.overlay.zzaa.zza(zzakaVar, "missingMimeTypes");
                return;
            }
            if (Build.VERSION.SDK_INT < 16) {
                zzafr.zzaT("Video decoder properties available on API versions >= 16.");
                com.google.android.gms.ads.internal.overlay.zzaa.zza(zzakaVar, "deficientApiVersion");
                return;
            }
            HashMap map2 = new HashMap();
            for (String str4 : str3.split(",")) {
                map2.put(str4, zzaiw.zzaQ(str4.trim()));
            }
            com.google.android.gms.ads.internal.overlay.zzaa.zzc(zzakaVar, map2);
            return;
        }
        zzajz zzajzVarZziE = zzakaVar.zziE();
        if (zzajzVarZziE == null) {
            zzafr.zzaT("Could not get underlay container for a video GMSG.");
            return;
        }
        boolean zEquals = "new".equals(str);
        boolean zEquals2 = "position".equals(str);
        if (zEquals || zEquals2) {
            Context context = zzakaVar.getContext();
            int iZza = zza(context, map, "x", 0);
            int iZza2 = zza(context, map, "y", 0);
            int iZza3 = zza(context, map, "w", -1);
            int iZza4 = zza(context, map, "h", -1);
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFB)).booleanValue()) {
                iMin = Math.min(iZza3, zzakaVar.getMeasuredWidth() - iZza);
                iZza4 = Math.min(iZza4, zzakaVar.getMeasuredHeight() - iZza2);
            } else {
                iMin = iZza3;
            }
            try {
                i = Integer.parseInt(map.get("player"));
            } catch (NumberFormatException e2) {
                i = 0;
            }
            boolean z = Boolean.parseBoolean(map.get("spherical"));
            if (zEquals && zzajzVarZziE.zzip() == null) {
                zzajzVarZziE.zza(iZza, iZza2, iMin, iZza4, i, z, new com.google.android.gms.ads.internal.overlay.zzaq(map.get("flags")));
                return;
            } else {
                zzajzVarZziE.zze(iZza, iZza2, iMin, iZza4);
                return;
            }
        }
        com.google.android.gms.ads.internal.overlay.zzaa zzaaVarZzip = zzajzVarZziE.zzip();
        if (zzaaVarZzip == null) {
            com.google.android.gms.ads.internal.overlay.zzaa.zzh(zzakaVar);
            return;
        }
        if ("click".equals(str)) {
            Context context2 = zzakaVar.getContext();
            int iZza5 = zza(context2, map, "x", 0);
            int iZza6 = zza(context2, map, "y", 0);
            long jUptimeMillis = SystemClock.uptimeMillis();
            MotionEvent motionEventObtain = MotionEvent.obtain(jUptimeMillis, jUptimeMillis, 0, iZza5, iZza6, 0);
            zzaaVarZzip.zze(motionEventObtain);
            motionEventObtain.recycle();
            return;
        }
        if ("currentTime".equals(str)) {
            String str5 = map.get("time");
            if (str5 == null) {
                zzafr.zzaT("Time parameter missing from currentTime video GMSG.");
                return;
            }
            try {
                zzaaVarZzip.seekTo((int) (Float.parseFloat(str5) * 1000.0f));
                return;
            } catch (NumberFormatException e3) {
                String strValueOf2 = String.valueOf(str5);
                zzafr.zzaT(strValueOf2.length() != 0 ? "Could not parse time parameter from currentTime video GMSG: ".concat(strValueOf2) : new String("Could not parse time parameter from currentTime video GMSG: "));
                return;
            }
        }
        if ("hide".equals(str)) {
            zzaaVarZzip.setVisibility(4);
            return;
        }
        if ("load".equals(str)) {
            zzaaVarZzip.zzfY();
            return;
        }
        if ("muted".equals(str)) {
            if (Boolean.parseBoolean(map.get("muted"))) {
                zzaaVarZzip.zzfZ();
                return;
            } else {
                zzaaVarZzip.zzga();
                return;
            }
        }
        if ("pause".equals(str)) {
            zzaaVarZzip.pause();
            return;
        }
        if ("play".equals(str)) {
            zzaaVarZzip.play();
            return;
        }
        if ("show".equals(str)) {
            zzaaVarZzip.setVisibility(0);
            return;
        }
        if ("src".equals(str)) {
            zzaaVarZzip.zzaq(map.get("src"));
            return;
        }
        if ("touchMove".equals(str)) {
            Context context3 = zzakaVar.getContext();
            zzaaVarZzip.zza(zza(context3, map, "dx", 0), zza(context3, map, "dy", 0));
            if (this.zzJK) {
                return;
            }
            zzakaVar.zziu().zzfQ();
            this.zzJK = true;
            return;
        }
        if (!"volume".equals(str)) {
            if ("watermark".equals(str)) {
                zzaaVarZzip.zzgb();
                return;
            } else {
                String strValueOf3 = String.valueOf(str);
                zzafr.zzaT(strValueOf3.length() != 0 ? "Unknown video action: ".concat(strValueOf3) : new String("Unknown video action: "));
                return;
            }
        }
        String str6 = map.get("volume");
        if (str6 == null) {
            zzafr.zzaT("Level parameter missing from volume video GMSG.");
            return;
        }
        try {
            zzaaVarZzip.zzb(Float.parseFloat(str6));
        } catch (NumberFormatException e4) {
            String strValueOf4 = String.valueOf(str6);
            zzafr.zzaT(strValueOf4.length() != 0 ? "Could not parse volume parameter from volume video GMSG: ".concat(strValueOf4) : new String("Could not parse volume parameter from volume video GMSG: "));
        }
    }
}
