package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Color;
import android.location.Location;
import android.os.Build;
import android.os.Bundle;
import android.os.Debug;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.comscore.android.id.IdHelperAndroid;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Locale;
import org.apache.http.client.methods.HttpHead;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzabt {
    private static final SimpleDateFormat zzUK = new SimpleDateFormat("yyyyMMdd", Locale.US);

    /* JADX WARN: Code duplicated, block: B:68:0x026e A[PHI: r18
  0x026e: PHI (r18v3 int) = (r18v2 int), (r18v5 int) binds: [B:46:0x011a, B:51:0x012b] A[DONT_GENERATE, DONT_INLINE]] */
    public static zzaai zza(Context context, zzaae zzaaeVar, String str) {
        String str2;
        long j;
        try {
            JSONObject jSONObject = new JSONObject(str);
            String strOptString = jSONObject.optString("ad_base_url", null);
            String strOptString2 = jSONObject.optString("ad_url", null);
            String strOptString3 = jSONObject.optString("ad_size", null);
            String strOptString4 = jSONObject.optString("ad_slot_size", strOptString3);
            boolean z = (zzaaeVar == null || zzaaeVar.zzSF == 0) ? false : true;
            String strOptString5 = jSONObject.optString("ad_json", null);
            if (strOptString5 == null) {
                strOptString5 = jSONObject.optString("ad_html", null);
            }
            if (strOptString5 == null) {
                strOptString5 = jSONObject.optString("body", null);
            }
            long j2 = -1;
            String strOptString6 = jSONObject.optString("debug_dialog", null);
            String strOptString7 = jSONObject.optString("debug_signals", null);
            long j3 = jSONObject.has("interstitial_timeout") ? (long) (jSONObject.getDouble("interstitial_timeout") * 1000.0d) : -1L;
            String strOptString8 = jSONObject.optString("orientation", null);
            int iZzhT = -1;
            if ("portrait".equals(strOptString8)) {
                iZzhT = com.google.android.gms.ads.internal.zzbs.zzbB().zzhU();
            } else if ("landscape".equals(strOptString8)) {
                iZzhT = com.google.android.gms.ads.internal.zzbs.zzbB().zzhT();
            }
            zzaai zzaaiVarZza = null;
            if (!TextUtils.isEmpty(strOptString5) || TextUtils.isEmpty(strOptString2)) {
                str2 = strOptString5;
            } else {
                zzaaiVarZza = zzabm.zza(zzaaeVar, context, zzaaeVar.zzvT.zzaP, strOptString2, null, null, null, null);
                strOptString = zzaaiVarZza.zzPi;
                str2 = zzaaiVarZza.body;
                j2 = zzaaiVarZza.zzTs;
            }
            if (str2 == null) {
                return new zzaai(0);
            }
            JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("click_urls");
            List<String> listZza = zzaaiVarZza == null ? null : zzaaiVarZza.zzMa;
            if (jSONArrayOptJSONArray != null) {
                listZza = zza(jSONArrayOptJSONArray, listZza);
            }
            JSONArray jSONArrayOptJSONArray2 = jSONObject.optJSONArray("impression_urls");
            List<String> listZza2 = zzaaiVarZza == null ? null : zzaaiVarZza.zzMb;
            if (jSONArrayOptJSONArray2 != null) {
                listZza2 = zza(jSONArrayOptJSONArray2, listZza2);
            }
            JSONArray jSONArrayOptJSONArray3 = jSONObject.optJSONArray("manual_impression_urls");
            List<String> listZza3 = zzaaiVarZza == null ? null : zzaaiVarZza.zzTq;
            if (jSONArrayOptJSONArray3 != null) {
                listZza3 = zza(jSONArrayOptJSONArray3, listZza3);
            }
            if (zzaaiVarZza == null) {
                j = j3;
            } else {
                if (zzaaiVarZza.orientation != -1) {
                    iZzhT = zzaaiVarZza.orientation;
                }
                if (zzaaiVarZza.zzTn > 0) {
                    j = zzaaiVarZza.zzTn;
                } else {
                    j = j3;
                }
            }
            String strOptString9 = jSONObject.optString("active_view");
            boolean zOptBoolean = jSONObject.optBoolean("ad_is_javascript", false);
            String strOptString10 = zOptBoolean ? jSONObject.optString("ad_passback_url", null) : null;
            boolean zOptBoolean2 = jSONObject.optBoolean("mediation", false);
            boolean zOptBoolean3 = jSONObject.optBoolean("custom_render_allowed", false);
            boolean zOptBoolean4 = jSONObject.optBoolean("content_url_opted_out", true);
            boolean zOptBoolean5 = jSONObject.optBoolean("content_vertical_opted_out", true);
            boolean zOptBoolean6 = jSONObject.optBoolean("prefetch", false);
            long jOptLong = jSONObject.optLong("refresh_interval_milliseconds", -1L);
            return new zzaai(zzaaeVar, strOptString, str2, listZza, listZza2, j, zOptBoolean2, jSONObject.optLong("mediation_config_cache_time_milliseconds", -1L), listZza3, jOptLong, iZzhT, strOptString3, j2, strOptString6, zOptBoolean, strOptString10, strOptString9, zOptBoolean3, z, zzaaeVar.zzSH, zOptBoolean4, zOptBoolean6, jSONObject.optString("gws_query_id", ""), "height".equals(jSONObject.optString("fluid", "")), jSONObject.optBoolean("native_express", false), zzaee.zza(jSONObject.optJSONArray("rewards")), zza(jSONObject.optJSONArray("video_start_urls"), (List<String>) null), zza(jSONObject.optJSONArray("video_complete_urls"), (List<String>) null), jSONObject.optBoolean("use_displayed_impression", false), zzaak.zze(jSONObject.optJSONObject("auto_protection_configuration")), zzaaeVar.zzSV, jSONObject.optString("set_cookie", ""), zza(jSONObject.optJSONArray("remote_ping_urls"), (List<String>) null), jSONObject.optBoolean("render_in_browser", zzaaeVar.zzMe), strOptString4, zzaeq.zzf(jSONObject.optJSONObject("safe_browsing")), strOptString7, zOptBoolean5, zzaaeVar.zzTh);
        } catch (JSONException e) {
            String strValueOf = String.valueOf(e.getMessage());
            zzafr.zzaT(strValueOf.length() != 0 ? "Could not parse the inline ad response: ".concat(strValueOf) : new String("Could not parse the inline ad response: "));
            return new zzaai(0);
        }
    }

    @Nullable
    private static List<String> zza(@Nullable JSONArray jSONArray, @Nullable List<String> list) throws JSONException {
        if (jSONArray == null) {
            return null;
        }
        if (list == null) {
            list = new LinkedList<>();
        }
        for (int i = 0; i < jSONArray.length(); i++) {
            list.add(jSONArray.getString(i));
        }
        return list;
    }

    @Nullable
    public static JSONObject zza(Context context, zzabk zzabkVar) {
        String str;
        String str2;
        String str3;
        zzaae zzaaeVar = zzabkVar.zzUj;
        Location location = zzabkVar.zzzV;
        zzacb zzacbVar = zzabkVar.zzUk;
        Bundle bundle = zzabkVar.zzSG;
        JSONObject jSONObject = zzabkVar.zzUl;
        try {
            HashMap map = new HashMap();
            map.put("extra_caps", com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFl));
            if (zzabkVar.zzSN.size() > 0) {
                map.put("eid", TextUtils.join(",", zzabkVar.zzSN));
            }
            if (zzaaeVar.zzSy != null) {
                map.put("ad_pos", zzaaeVar.zzSy);
            }
            zzir zzirVar = zzaaeVar.zzSz;
            String strZzhK = zzafo.zzhK();
            if (strZzhK != null) {
                map.put("abf", strZzhK);
            }
            if (zzirVar.zzzN != -1) {
                map.put("cust_age", zzUK.format(new Date(zzirVar.zzzN)));
            }
            if (zzirVar.extras != null) {
                map.put("extras", zzirVar.extras);
            }
            if (zzirVar.zzzO != -1) {
                map.put("cust_gender", Integer.valueOf(zzirVar.zzzO));
            }
            if (zzirVar.zzzP != null) {
                map.put("kw", zzirVar.zzzP);
            }
            if (zzirVar.zzzR != -1) {
                map.put("tag_for_child_directed_treatment", Integer.valueOf(zzirVar.zzzR));
            }
            if (zzirVar.zzzQ) {
                map.put("adtest", "on");
            }
            if (zzirVar.versionCode >= 2) {
                if (zzirVar.zzzS) {
                    map.put("d_imp_hdr", 1);
                }
                if (!TextUtils.isEmpty(zzirVar.zzzT)) {
                    map.put("ppid", zzirVar.zzzT);
                }
                if (zzirVar.zzzU != null) {
                    zzlt zzltVar = zzirVar.zzzU;
                    if (Color.alpha(zzltVar.zzBw) != 0) {
                        map.put("acolor", zzu(zzltVar.zzBw));
                    }
                    if (Color.alpha(zzltVar.backgroundColor) != 0) {
                        map.put("bgcolor", zzu(zzltVar.backgroundColor));
                    }
                    if (Color.alpha(zzltVar.zzBx) != 0 && Color.alpha(zzltVar.zzBy) != 0) {
                        map.put("gradientto", zzu(zzltVar.zzBx));
                        map.put("gradientfrom", zzu(zzltVar.zzBy));
                    }
                    if (Color.alpha(zzltVar.zzBz) != 0) {
                        map.put("bcolor", zzu(zzltVar.zzBz));
                    }
                    map.put("bthick", Integer.toString(zzltVar.zzBA));
                    switch (zzltVar.zzBB) {
                        case 0:
                            str2 = IdHelperAndroid.NO_ID_AVAILABLE;
                            break;
                        case 1:
                            str2 = "dashed";
                            break;
                        case 2:
                            str2 = "dotted";
                            break;
                        case 3:
                            str2 = "solid";
                            break;
                        default:
                            str2 = null;
                            break;
                    }
                    if (str2 != null) {
                        map.put("btype", str2);
                    }
                    switch (zzltVar.zzBC) {
                        case 0:
                            str3 = "light";
                            break;
                        case 1:
                            str3 = "medium";
                            break;
                        case 2:
                            str3 = "dark";
                            break;
                        default:
                            str3 = null;
                            break;
                    }
                    if (str3 != null) {
                        map.put("callbuttoncolor", str3);
                    }
                    if (zzltVar.zzBD != null) {
                        map.put("channel", zzltVar.zzBD);
                    }
                    if (Color.alpha(zzltVar.zzBE) != 0) {
                        map.put("dcolor", zzu(zzltVar.zzBE));
                    }
                    if (zzltVar.zzBF != null) {
                        map.put("font", zzltVar.zzBF);
                    }
                    if (Color.alpha(zzltVar.zzBG) != 0) {
                        map.put("hcolor", zzu(zzltVar.zzBG));
                    }
                    map.put("headersize", Integer.toString(zzltVar.zzBH));
                    if (zzltVar.zzBI != null) {
                        map.put("q", zzltVar.zzBI);
                    }
                }
            }
            if (zzirVar.versionCode >= 3 && zzirVar.zzzW != null) {
                map.put("url", zzirVar.zzzW);
            }
            if (zzirVar.versionCode >= 5) {
                if (zzirVar.zzzY != null) {
                    map.put("custom_targeting", zzirVar.zzzY);
                }
                if (zzirVar.zzzZ != null) {
                    map.put("category_exclusions", zzirVar.zzzZ);
                }
                if (zzirVar.zzAa != null) {
                    map.put("request_agent", zzirVar.zzAa);
                }
            }
            if (zzirVar.versionCode >= 6 && zzirVar.zzAb != null) {
                map.put("request_pkg", zzirVar.zzAb);
            }
            if (zzirVar.versionCode >= 7) {
                map.put("is_designed_for_families", Boolean.valueOf(zzirVar.zzAc));
            }
            if (zzaaeVar.zzvX.zzAu == null) {
                map.put("format", zzaaeVar.zzvX.zzAs);
                if (zzaaeVar.zzvX.zzAw) {
                    map.put("fluid", "height");
                }
            } else {
                boolean z = false;
                boolean z2 = false;
                for (zziv zzivVar : zzaaeVar.zzvX.zzAu) {
                    if (!zzivVar.zzAw && !z2) {
                        map.put("format", zzivVar.zzAs);
                        z2 = true;
                    }
                    if (zzivVar.zzAw && !z) {
                        map.put("fluid", "height");
                        z = true;
                    }
                    if (!z2 || !z) {
                    }
                }
            }
            if (zzaaeVar.zzvX.width == -1) {
                map.put("smart_w", "full");
            }
            if (zzaaeVar.zzvX.height == -2) {
                map.put("smart_h", "auto");
            }
            if (zzaaeVar.zzvX.zzAu != null) {
                StringBuilder sb = new StringBuilder();
                boolean z3 = false;
                for (zziv zzivVar2 : zzaaeVar.zzvX.zzAu) {
                    if (zzivVar2.zzAw) {
                        z3 = true;
                    } else {
                        if (sb.length() != 0) {
                            sb.append("|");
                        }
                        sb.append(zzivVar2.width == -1 ? (int) (zzivVar2.widthPixels / zzacbVar.zzxR) : zzivVar2.width);
                        sb.append("x");
                        sb.append(zzivVar2.height == -2 ? (int) (zzivVar2.heightPixels / zzacbVar.zzxR) : zzivVar2.height);
                    }
                }
                if (z3) {
                    if (sb.length() != 0) {
                        sb.insert(0, "|");
                    }
                    sb.insert(0, "320x50");
                }
                map.put("sz", sb);
            }
            if (zzaaeVar.zzSF != 0) {
                map.put("native_version", Integer.valueOf(zzaaeVar.zzSF));
                map.put("native_templates", zzaaeVar.zzwq);
                zzon zzonVar = zzaaeVar.zzwj;
                switch (zzonVar != null ? zzonVar.zzIo : 0) {
                    case 0:
                        str = "any";
                        break;
                    case 1:
                        str = "portrait";
                        break;
                    case 2:
                        str = "landscape";
                        break;
                    default:
                        str = "not_set";
                        break;
                }
                map.put("native_image_orientation", str);
                if (!zzaaeVar.zzSO.isEmpty()) {
                    map.put("native_custom_templates", zzaaeVar.zzSO);
                }
                if (!TextUtils.isEmpty(zzaaeVar.zzTi)) {
                    try {
                        map.put("native_advanced_settings", new JSONArray(zzaaeVar.zzTi));
                    } catch (JSONException e) {
                        zzafr.zzc("Problem creating json from native advanced settings", e);
                    }
                }
            }
            if (zzaaeVar.zzwn != null && zzaaeVar.zzwn.size() > 0) {
                for (Integer num : zzaaeVar.zzwn) {
                    if (num.intValue() == 2) {
                        map.put("iba", true);
                    } else if (num.intValue() == 1) {
                        map.put("ina", true);
                    }
                }
            }
            if (zzaaeVar.zzvX.zzAx) {
                map.put("ene", true);
            }
            if (zzaaeVar.zzwl != null) {
                map.put("is_icon_ad", true);
                map.put("icon_ad_expansion_behavior", Integer.valueOf(zzaaeVar.zzwl.zzAR));
            }
            map.put("slotname", zzaaeVar.zzvR);
            map.put("pn", zzaaeVar.applicationInfo.packageName);
            if (zzaaeVar.zzSA != null) {
                map.put("vc", Integer.valueOf(zzaaeVar.zzSA.versionCode));
            }
            map.put("ms", zzabkVar.zzSB);
            map.put("seq_num", zzaaeVar.zzSC);
            map.put("session_id", zzaaeVar.zzSD);
            map.put("js", zzaaeVar.zzvT.zzaP);
            zzacn zzacnVar = zzabkVar.zzUh;
            Bundle bundle2 = zzaaeVar.zzTa;
            Bundle bundle3 = zzabkVar.zzUg;
            map.put("am", Integer.valueOf(zzacbVar.zzVF));
            map.put("cog", zzt(zzacbVar.zzVG));
            map.put("coh", zzt(zzacbVar.zzVH));
            if (!TextUtils.isEmpty(zzacbVar.zzVI)) {
                map.put("carrier", zzacbVar.zzVI);
            }
            map.put("gl", zzacbVar.zzVJ);
            if (zzacbVar.zzVK) {
                map.put("simulator", 1);
            }
            if (zzacbVar.zzVL) {
                map.put("is_sidewinder", 1);
            }
            map.put("ma", zzt(zzacbVar.zzVM));
            map.put("sp", zzt(zzacbVar.zzVN));
            map.put("hl", zzacbVar.zzVO);
            if (!TextUtils.isEmpty(zzacbVar.zzVP)) {
                map.put("mv", zzacbVar.zzVP);
            }
            map.put("muv", Integer.valueOf(zzacbVar.zzVR));
            if (zzacbVar.zzVS != -2) {
                map.put("cnt", Integer.valueOf(zzacbVar.zzVS));
            }
            map.put("gnt", Integer.valueOf(zzacbVar.zzVT));
            map.put("pt", Integer.valueOf(zzacbVar.zzVU));
            map.put("rm", Integer.valueOf(zzacbVar.zzVV));
            map.put("riv", Integer.valueOf(zzacbVar.zzVW));
            Bundle bundle4 = new Bundle();
            bundle4.putString("build_build", zzacbVar.zzWb);
            bundle4.putString("build_device", zzacbVar.zzWc);
            Bundle bundle5 = new Bundle();
            bundle5.putBoolean("is_charging", zzacbVar.zzVY);
            bundle5.putDouble("battery_level", zzacbVar.zzVX);
            bundle4.putBundle("battery", bundle5);
            Bundle bundle6 = new Bundle();
            bundle6.putInt("active_network_state", zzacbVar.zzWa);
            bundle6.putBoolean("active_network_metered", zzacbVar.zzVZ);
            if (zzacnVar != null) {
                Bundle bundle7 = new Bundle();
                bundle7.putInt("predicted_latency_micros", zzacnVar.zzWm);
                bundle7.putLong("predicted_down_throughput_bps", zzacnVar.zzWn);
                bundle7.putLong("predicted_up_throughput_bps", zzacnVar.zzWo);
                bundle6.putBundle("predictions", bundle7);
            }
            bundle4.putBundle("network", bundle6);
            Bundle bundle8 = new Bundle();
            bundle8.putBoolean("is_browser_custom_tabs_capable", zzacbVar.zzWd);
            bundle4.putBundle("browser", bundle8);
            if (bundle2 != null) {
                Bundle bundle9 = new Bundle();
                bundle9.putString("runtime_free", Long.toString(bundle2.getLong("runtime_free_memory", -1L)));
                bundle9.putString("runtime_max", Long.toString(bundle2.getLong("runtime_max_memory", -1L)));
                bundle9.putString("runtime_total", Long.toString(bundle2.getLong("runtime_total_memory", -1L)));
                bundle9.putString("web_view_count", Integer.toString(bundle2.getInt("web_view_count", 0)));
                Debug.MemoryInfo memoryInfo = (Debug.MemoryInfo) bundle2.getParcelable("debug_memory_info");
                if (memoryInfo != null) {
                    bundle9.putString("debug_info_dalvik_private_dirty", Integer.toString(memoryInfo.dalvikPrivateDirty));
                    bundle9.putString("debug_info_dalvik_pss", Integer.toString(memoryInfo.dalvikPss));
                    bundle9.putString("debug_info_dalvik_shared_dirty", Integer.toString(memoryInfo.dalvikSharedDirty));
                    bundle9.putString("debug_info_native_private_dirty", Integer.toString(memoryInfo.nativePrivateDirty));
                    bundle9.putString("debug_info_native_pss", Integer.toString(memoryInfo.nativePss));
                    bundle9.putString("debug_info_native_shared_dirty", Integer.toString(memoryInfo.nativeSharedDirty));
                    bundle9.putString("debug_info_other_private_dirty", Integer.toString(memoryInfo.otherPrivateDirty));
                    bundle9.putString("debug_info_other_pss", Integer.toString(memoryInfo.otherPss));
                    bundle9.putString("debug_info_other_shared_dirty", Integer.toString(memoryInfo.otherSharedDirty));
                }
                bundle4.putBundle("android_mem_info", bundle9);
            }
            Bundle bundle10 = new Bundle();
            bundle10.putBundle("parental_controls", bundle3);
            if (!TextUtils.isEmpty(zzacbVar.zzVQ)) {
                bundle10.putString("package_version", zzacbVar.zzVQ);
            }
            bundle4.putBundle("play_store", bundle10);
            map.put("device", bundle4);
            Bundle bundle11 = new Bundle();
            bundle11.putString("doritos", zzabkVar.zzUi);
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDM)).booleanValue()) {
                String id = null;
                boolean zIsLimitAdTrackingEnabled = false;
                if (zzabkVar.zzqi != null) {
                    id = zzabkVar.zzqi.getId();
                    zIsLimitAdTrackingEnabled = zzabkVar.zzqi.isLimitAdTrackingEnabled();
                }
                if (TextUtils.isEmpty(id)) {
                    zzji.zzds();
                    bundle11.putString("pdid", zzaiy.zzW(context));
                    bundle11.putString("pdidtype", "ssaid");
                } else {
                    bundle11.putString("rdid", id);
                    bundle11.putBoolean("is_lat", zIsLimitAdTrackingEnabled);
                    bundle11.putString("idtype", "adid");
                }
            }
            map.put("pii", bundle11);
            map.put("platform", Build.MANUFACTURER);
            map.put("submodel", Build.MODEL);
            if (location != null) {
                zza((HashMap<String, Object>) map, location);
            } else if (zzaaeVar.zzSz.versionCode >= 2 && zzaaeVar.zzSz.zzzV != null) {
                zza((HashMap<String, Object>) map, zzaaeVar.zzSz.zzzV);
            }
            if (zzaaeVar.versionCode >= 2) {
                map.put("quality_signals", zzaaeVar.zzSE);
            }
            if (zzaaeVar.versionCode >= 4 && zzaaeVar.zzSH) {
                map.put("forceHttps", Boolean.valueOf(zzaaeVar.zzSH));
            }
            if (bundle != null) {
                map.put("content_info", bundle);
            }
            if (zzaaeVar.versionCode >= 5) {
                map.put("u_sd", Float.valueOf(zzaaeVar.zzxR));
                map.put("sh", Integer.valueOf(zzaaeVar.zzSJ));
                map.put("sw", Integer.valueOf(zzaaeVar.zzSI));
            } else {
                map.put("u_sd", Float.valueOf(zzacbVar.zzxR));
                map.put("sh", Integer.valueOf(zzacbVar.zzSJ));
                map.put("sw", Integer.valueOf(zzacbVar.zzSI));
            }
            if (zzaaeVar.versionCode >= 6) {
                if (!TextUtils.isEmpty(zzaaeVar.zzSK)) {
                    try {
                        map.put("view_hierarchy", new JSONObject(zzaaeVar.zzSK));
                    } catch (JSONException e2) {
                        zzafr.zzc("Problem serializing view hierarchy to JSON", e2);
                    }
                }
                map.put("correlation_id", Long.valueOf(zzaaeVar.zzSL));
            }
            if (zzaaeVar.versionCode >= 7) {
                map.put("request_id", zzaaeVar.zzSM);
            }
            if (zzaaeVar.versionCode >= 12 && !TextUtils.isEmpty(zzaaeVar.zzSQ)) {
                map.put("anchor", zzaaeVar.zzSQ);
            }
            if (zzaaeVar.versionCode >= 13) {
                map.put("android_app_volume", Float.valueOf(zzaaeVar.zzSR));
            }
            if (zzaaeVar.versionCode >= 18) {
                map.put("android_app_muted", Boolean.valueOf(zzaaeVar.zzSX));
            }
            if (zzaaeVar.versionCode >= 14 && zzaaeVar.zzSS > 0) {
                map.put("target_api", Integer.valueOf(zzaaeVar.zzSS));
            }
            if (zzaaeVar.versionCode >= 15) {
                map.put("scroll_index", Integer.valueOf(zzaaeVar.zzST == -1 ? -1 : zzaaeVar.zzST));
            }
            if (zzaaeVar.versionCode >= 16) {
                map.put("_activity_context", Boolean.valueOf(zzaaeVar.zzSU));
            }
            if (zzaaeVar.versionCode >= 18) {
                if (!TextUtils.isEmpty(zzaaeVar.zzSY)) {
                    try {
                        map.put("app_settings", new JSONObject(zzaaeVar.zzSY));
                    } catch (JSONException e3) {
                        zzafr.zzc("Problem creating json from app settings", e3);
                    }
                }
                map.put("render_in_browser", Boolean.valueOf(zzaaeVar.zzMe));
            }
            if (zzaaeVar.versionCode >= 18) {
                map.put("android_num_video_cache_tasks", Integer.valueOf(zzaaeVar.zzSZ));
            }
            zzaje zzajeVar = zzaaeVar.zzvT;
            boolean z4 = zzabkVar.zzUm;
            Bundle bundle12 = new Bundle();
            Bundle bundle13 = new Bundle();
            bundle13.putString("cl", "162978962");
            bundle13.putString("rapid_rc", "dev");
            bundle13.putString("rapid_rollup", HttpHead.METHOD_NAME);
            bundle12.putBundle("build_meta", bundle13);
            bundle12.putString("mf", Boolean.toString(((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFn)).booleanValue()));
            bundle12.putBoolean("instant_app", zzbha.zzaP(context).zzsl());
            bundle12.putBoolean("lite", zzajeVar.zzaaR);
            bundle12.putBoolean("local_service", z4);
            map.put("sdk_env", bundle12);
            map.put("cache_state", jSONObject);
            if (zzaaeVar.versionCode >= 19) {
                map.put("gct", zzaaeVar.zzTb);
            }
            if (zzaaeVar.versionCode >= 21 && zzaaeVar.zzTc) {
                map.put("de", "1");
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDY)).booleanValue()) {
                String str4 = zzaaeVar.zzvX.zzAs;
                boolean z5 = str4.equals("interstitial_mb") || str4.equals("reward_mb");
                Bundle bundle14 = zzaaeVar.zzTd;
                boolean z6 = bundle14 != null;
                if (z5 && z6) {
                    Bundle bundle15 = new Bundle();
                    bundle15.putBundle("interstitial_pool", bundle14);
                    map.put("counters", bundle15);
                }
            }
            if (zzaaeVar.versionCode >= 22 && com.google.android.gms.ads.internal.zzbs.zzbY().zzp(context)) {
                map.put("gmp_app_id", zzaaeVar.zzTe);
                if ("TIME_OUT".equals(zzaaeVar.zzTf)) {
                    map.put("sai_timeout", com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDB));
                } else if (zzaaeVar.zzTf == null) {
                    map.put("fbs_aiid", "");
                } else {
                    map.put("fbs_aiid", zzaaeVar.zzTf);
                }
                map.put("fbs_aeid", zzaaeVar.zzTg);
            }
            String str5 = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCE);
            if (str5 != null && !str5.isEmpty()) {
                if (Build.VERSION.SDK_INT >= ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCF)).intValue()) {
                    HashMap map2 = new HashMap();
                    String[] strArrSplit = str5.split(",");
                    for (String str6 : strArrSplit) {
                        map2.put(str6, zzaiw.zzaQ(str6));
                    }
                    map.put("video_decoders", map2);
                }
            }
            if (zzafr.zzz(2)) {
                String strValueOf = String.valueOf(com.google.android.gms.ads.internal.zzbs.zzbz().zzj(map).toString(2));
                zzafr.v(strValueOf.length() != 0 ? "Ad Request JSON: ".concat(strValueOf) : new String("Ad Request JSON: "));
            }
            return com.google.android.gms.ads.internal.zzbs.zzbz().zzj(map);
        } catch (JSONException e4) {
            String strValueOf2 = String.valueOf(e4.getMessage());
            zzafr.zzaT(strValueOf2.length() != 0 ? "Problem serializing ad request to JSON: ".concat(strValueOf2) : new String("Problem serializing ad request to JSON: "));
            return null;
        }
    }

    private static void zza(HashMap<String, Object> map, Location location) {
        HashMap map2 = new HashMap();
        Float fValueOf = Float.valueOf(location.getAccuracy() * 1000.0f);
        Long lValueOf = Long.valueOf(location.getTime() * 1000);
        Long lValueOf2 = Long.valueOf((long) (location.getLatitude() * 1.0E7d));
        Long lValueOf3 = Long.valueOf((long) (location.getLongitude() * 1.0E7d));
        map2.put("radius", fValueOf);
        map2.put("lat", lValueOf2);
        map2.put("long", lValueOf3);
        map2.put("time", lValueOf);
        map.put("uule", map2);
    }

    public static JSONObject zzb(zzaai zzaaiVar) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        if (zzaaiVar.zzPi != null) {
            jSONObject.put("ad_base_url", zzaaiVar.zzPi);
        }
        if (zzaaiVar.zzTr != null) {
            jSONObject.put("ad_size", zzaaiVar.zzTr);
        }
        jSONObject.put("native", zzaaiVar.zzAv);
        if (zzaaiVar.zzAv) {
            jSONObject.put("ad_json", zzaaiVar.body);
        } else {
            jSONObject.put("ad_html", zzaaiVar.body);
        }
        if (zzaaiVar.zzTt != null) {
            jSONObject.put("debug_dialog", zzaaiVar.zzTt);
        }
        if (zzaaiVar.zzTK != null) {
            jSONObject.put("debug_signals", zzaaiVar.zzTK);
        }
        if (zzaaiVar.zzTn != -1) {
            jSONObject.put("interstitial_timeout", zzaaiVar.zzTn / 1000.0d);
        }
        if (zzaaiVar.orientation == com.google.android.gms.ads.internal.zzbs.zzbB().zzhU()) {
            jSONObject.put("orientation", "portrait");
        } else if (zzaaiVar.orientation == com.google.android.gms.ads.internal.zzbs.zzbB().zzhT()) {
            jSONObject.put("orientation", "landscape");
        }
        if (zzaaiVar.zzMa != null) {
            jSONObject.put("click_urls", zzm(zzaaiVar.zzMa));
        }
        if (zzaaiVar.zzMb != null) {
            jSONObject.put("impression_urls", zzm(zzaaiVar.zzMb));
        }
        if (zzaaiVar.zzTq != null) {
            jSONObject.put("manual_impression_urls", zzm(zzaaiVar.zzTq));
        }
        if (zzaaiVar.zzTw != null) {
            jSONObject.put("active_view", zzaaiVar.zzTw);
        }
        jSONObject.put("ad_is_javascript", zzaaiVar.zzTu);
        if (zzaaiVar.zzTv != null) {
            jSONObject.put("ad_passback_url", zzaaiVar.zzTv);
        }
        jSONObject.put("mediation", zzaaiVar.zzTo);
        jSONObject.put("custom_render_allowed", zzaaiVar.zzTx);
        jSONObject.put("content_url_opted_out", zzaaiVar.zzTy);
        jSONObject.put("content_vertical_opted_out", zzaaiVar.zzTL);
        jSONObject.put("prefetch", zzaaiVar.zzTz);
        if (zzaaiVar.zzMg != -1) {
            jSONObject.put("refresh_interval_milliseconds", zzaaiVar.zzMg);
        }
        if (zzaaiVar.zzTp != -1) {
            jSONObject.put("mediation_config_cache_time_milliseconds", zzaaiVar.zzTp);
        }
        if (!TextUtils.isEmpty(zzaaiVar.zzTC)) {
            jSONObject.put("gws_query_id", zzaaiVar.zzTC);
        }
        jSONObject.put("fluid", zzaaiVar.zzAw ? "height" : "");
        jSONObject.put("native_express", zzaaiVar.zzAx);
        if (zzaaiVar.zzTE != null) {
            jSONObject.put("video_start_urls", zzm(zzaaiVar.zzTE));
        }
        if (zzaaiVar.zzTF != null) {
            jSONObject.put("video_complete_urls", zzm(zzaaiVar.zzTF));
        }
        if (zzaaiVar.zzTD != null) {
            zzaee zzaeeVar = zzaaiVar.zzTD;
            JSONObject jSONObject2 = new JSONObject();
            jSONObject2.put("rb_type", zzaeeVar.type);
            jSONObject2.put("rb_amount", zzaeeVar.zzWW);
            JSONArray jSONArray = new JSONArray();
            jSONArray.put(jSONObject2);
            jSONObject.put("rewards", jSONArray);
        }
        jSONObject.put("use_displayed_impression", zzaaiVar.zzTG);
        jSONObject.put("auto_protection_configuration", zzaaiVar.zzTH);
        jSONObject.put("render_in_browser", zzaaiVar.zzMe);
        return jSONObject;
    }

    @Nullable
    private static JSONArray zzm(List<String> list) throws JSONException {
        JSONArray jSONArray = new JSONArray();
        Iterator<String> it = list.iterator();
        while (it.hasNext()) {
            jSONArray.put(it.next());
        }
        return jSONArray;
    }

    private static Integer zzt(boolean z) {
        return Integer.valueOf(z ? 1 : 0);
    }

    private static String zzu(int i) {
        return String.format(Locale.US, "#%06x", Integer.valueOf(16777215 & i));
    }
}
