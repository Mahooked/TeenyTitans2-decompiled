package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqm implements zzrd {
    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("action");
        if ("tick".equals(str)) {
            String str2 = map.get("label");
            String str3 = map.get("start_label");
            String str4 = map.get("timestamp");
            if (TextUtils.isEmpty(str2)) {
                zzafr.zzaT("No label given for CSI tick.");
                return;
            }
            if (TextUtils.isEmpty(str4)) {
                zzafr.zzaT("No timestamp given for CSI tick.");
                return;
            }
            try {
                long j = (Long.parseLong(str4) - com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis()) + com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
                if (TextUtils.isEmpty(str3)) {
                    str3 = "native:view_load";
                }
                zzakaVar.zziG().zza(str2, str3, j);
                return;
            } catch (NumberFormatException e) {
                zzafr.zzc("Malformed timestamp for CSI tick.", e);
                return;
            }
        }
        if ("experiment".equals(str)) {
            String str5 = map.get("value");
            if (TextUtils.isEmpty(str5)) {
                zzafr.zzaT("No value given for CSI experiment.");
                return;
            }
            zznb zznbVarZzdR = zzakaVar.zziG().zzdR();
            if (zznbVarZzdR == null) {
                zzafr.zzaT("No ticker for WebView, dropping experiment ID.");
                return;
            } else {
                zznbVarZzdR.zzh("e", str5);
                return;
            }
        }
        if ("extra".equals(str)) {
            String str6 = map.get("name");
            String str7 = map.get("value");
            if (TextUtils.isEmpty(str7)) {
                zzafr.zzaT("No value given for CSI extra.");
                return;
            }
            if (TextUtils.isEmpty(str6)) {
                zzafr.zzaT("No name given for CSI extra.");
                return;
            }
            zznb zznbVarZzdR2 = zzakaVar.zziG().zzdR();
            if (zznbVarZzdR2 == null) {
                zzafr.zzaT("No ticker for WebView, dropping extra parameter.");
            } else {
                zznbVarZzdR2.zzh(str6, str7);
            }
        }
    }
}
