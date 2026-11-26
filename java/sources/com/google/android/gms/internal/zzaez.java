package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaez {
    public static String zzb(String str, Context context) {
        String strZzw;
        if (!com.google.android.gms.ads.internal.zzbs.zzbY().zzp(context) || TextUtils.isEmpty(str) || (strZzw = com.google.android.gms.ads.internal.zzbs.zzbY().zzw(context)) == null) {
            return str;
        }
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDz)).booleanValue()) {
            String str2 = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDA);
            if (!str.contains(str2)) {
                return str;
            }
            com.google.android.gms.ads.internal.zzbs.zzbz();
            if (zzagz.zzaL(str)) {
                com.google.android.gms.ads.internal.zzbs.zzbY().zzf(context, strZzw);
                return str.replaceAll(str2, strZzw);
            }
            com.google.android.gms.ads.internal.zzbs.zzbz();
            if (!zzagz.zzaM(str)) {
                return str;
            }
            com.google.android.gms.ads.internal.zzbs.zzbY().zzg(context, strZzw);
            return str.replaceAll(str2, strZzw);
        }
        if (str.contains("fbs_aeid")) {
            return str;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (zzagz.zzaL(str)) {
            com.google.android.gms.ads.internal.zzbs.zzbY().zzf(context, strZzw);
            com.google.android.gms.ads.internal.zzbs.zzbz();
            return zzagz.zzb(str, "fbs_aeid", strZzw).toString();
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (!zzagz.zzaM(str)) {
            return str;
        }
        com.google.android.gms.ads.internal.zzbs.zzbY().zzg(context, strZzw);
        com.google.android.gms.ads.internal.zzbs.zzbz();
        return zzagz.zzb(str, "fbs_aeid", strZzw).toString();
    }
}
