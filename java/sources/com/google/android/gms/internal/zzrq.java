package com.google.android.gms.internal;

import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.text.TextUtils;
import java.net.URISyntaxException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzrq implements zzrd {
    private final com.google.android.gms.ads.internal.zzw zzJE;
    private final zzwk zzJF;

    public zzrq(com.google.android.gms.ads.internal.zzw zzwVar, zzwk zzwkVar) {
        this.zzJE = zzwVar;
        this.zzJF = zzwkVar;
    }

    private static boolean zze(Map<String, String> map) {
        return "1".equals(map.get("custom_close"));
    }

    private static int zzf(Map<String, String> map) {
        String str = map.get("o");
        if (str != null) {
            if ("p".equalsIgnoreCase(str)) {
                return com.google.android.gms.ads.internal.zzbs.zzbB().zzhU();
            }
            if ("l".equalsIgnoreCase(str)) {
                return com.google.android.gms.ads.internal.zzbs.zzbB().zzhT();
            }
            if ("c".equalsIgnoreCase(str)) {
                return com.google.android.gms.ads.internal.zzbs.zzbB().zzhV();
            }
        }
        return -1;
    }

    private final void zzj(boolean z) {
        if (this.zzJF != null) {
            this.zzJF.zzk(z);
        }
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) throws URISyntaxException {
        Intent uri;
        Uri uri2;
        String strZzb = zzaez.zzb(map.get("u"), zzakaVar.getContext());
        String str = map.get("a");
        if (str == null) {
            zzafr.zzaT("Action missing from an open GMSG.");
            return;
        }
        if (this.zzJE != null && !this.zzJE.zzaR()) {
            this.zzJE.zzt(strZzb);
            return;
        }
        zzakb zzakbVarZziw = zzakaVar.zziw();
        if ("expand".equalsIgnoreCase(str)) {
            if (zzakaVar.zziA()) {
                zzafr.zzaT("Cannot expand WebView that is already expanded.");
                return;
            } else {
                zzj(false);
                zzakbVarZziw.zza(zze(map), zzf(map));
                return;
            }
        }
        if ("webapp".equalsIgnoreCase(str)) {
            zzj(false);
            if (strZzb != null) {
                zzakbVarZziw.zza(zze(map), zzf(map), strZzb);
                return;
            } else {
                zzakbVarZziw.zza(zze(map), zzf(map), map.get("html"), map.get("baseurl"));
                return;
            }
        }
        if ("in_app_purchase".equalsIgnoreCase(str)) {
            return;
        }
        if ("app".equalsIgnoreCase(str) && "true".equalsIgnoreCase(map.get("system_browser"))) {
            zzj(true);
            Context context = zzakaVar.getContext();
            if (TextUtils.isEmpty(strZzb)) {
                zzafr.zzaT("Destination url cannot be empty.");
                return;
            }
            try {
                zzakaVar.zziw().zza(new com.google.android.gms.ads.internal.overlay.zzc(new zzrr(zzakaVar).zza(context, map)));
                return;
            } catch (ActivityNotFoundException e) {
                zzafr.zzaT(e.getMessage());
                return;
            }
        }
        zzj(true);
        String str2 = map.get("intent_url");
        if (TextUtils.isEmpty(str2)) {
            uri = null;
        } else {
            try {
                uri = Intent.parseUri(str2, 0);
            } catch (URISyntaxException e2) {
                String strValueOf = String.valueOf(str2);
                zzafr.zzb(strValueOf.length() != 0 ? "Error parsing the url: ".concat(strValueOf) : new String("Error parsing the url: "), e2);
                uri = null;
            }
        }
        if (uri != null && uri.getData() != null) {
            Uri data = uri.getData();
            String string = data.toString();
            if (TextUtils.isEmpty(string)) {
                uri2 = data;
            } else {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                String strZzb2 = zzagz.zzb(zzakaVar, string);
                try {
                    uri2 = Uri.parse(strZzb2);
                } catch (Exception e3) {
                    String strValueOf2 = String.valueOf(strZzb2);
                    zzafr.zzb(strValueOf2.length() != 0 ? "Error parsing the uri: ".concat(strValueOf2) : new String("Error parsing the uri: "), e3);
                    uri2 = data;
                }
            }
            uri.setData(uri2);
        }
        if (uri != null) {
            zzakbVarZziw.zza(new com.google.android.gms.ads.internal.overlay.zzc(uri));
            return;
        }
        if (!TextUtils.isEmpty(strZzb)) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            strZzb = zzagz.zzb(zzakaVar, strZzb);
        }
        zzakbVarZziw.zza(new com.google.android.gms.ads.internal.overlay.zzc(map.get("i"), strZzb, map.get("m"), map.get("p"), map.get("c"), map.get("f"), map.get("e")));
    }
}
