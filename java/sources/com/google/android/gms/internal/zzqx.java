package com.google.android.gms.internal;

import android.net.Uri;
import android.text.TextUtils;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqx implements zzrd {
    zzqx() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("u");
        if (str == null) {
            zzafr.zzaT("URL missing from click GMSG.");
            return;
        }
        Uri uriZzb = Uri.parse(str);
        try {
            zzcu zzcuVarZziy = zzakaVar.zziy();
            uriZzb = (zzcuVarZziy == null || !zzcuVarZziy.zzc(uriZzb)) ? uriZzb : zzcuVarZziy.zza(uriZzb, zzakaVar.getContext(), zzakaVar.getView());
        } catch (zzcv e) {
            String strValueOf = String.valueOf(str);
            zzafr.zzaT(strValueOf.length() != 0 ? "Unable to append parameter to URL: ".concat(strValueOf) : new String("Unable to append parameter to URL: "));
        }
        if ((((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDy)).booleanValue() && com.google.android.gms.ads.internal.zzbs.zzbY().zzp(zzakaVar.getContext())) && TextUtils.isEmpty(uriZzb.getQueryParameter("fbs_aeid"))) {
            String strZzw = com.google.android.gms.ads.internal.zzbs.zzbY().zzw(zzakaVar.getContext());
            com.google.android.gms.ads.internal.zzbs.zzbz();
            uriZzb = zzagz.zzb(uriZzb.toString(), "fbs_aeid", strZzw);
            com.google.android.gms.ads.internal.zzbs.zzbY().zzf(zzakaVar.getContext(), strZzw);
        }
        new zzaiq(zzakaVar.getContext(), zzakaVar.zziz().zzaP, uriZzb.toString()).zzhL();
    }
}
