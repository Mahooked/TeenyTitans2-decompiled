package com.google.android.gms.internal;

import android.content.Context;
import android.os.Build;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmq {
    private Context mContext;
    private boolean zzGI = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCQ)).booleanValue();
    private String zzGJ = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCR);
    private Map<String, String> zzGK = new LinkedHashMap();
    private String zzwH;

    public zzmq(Context context, String str) {
        this.mContext = null;
        this.zzwH = null;
        this.mContext = context;
        this.zzwH = str;
        this.zzGK.put("s", "gmob_sdk");
        this.zzGK.put("v", "3");
        this.zzGK.put("os", Build.VERSION.RELEASE);
        this.zzGK.put("sdk", Build.VERSION.SDK);
        Map<String, String> map = this.zzGK;
        com.google.android.gms.ads.internal.zzbs.zzbz();
        map.put("device", zzagz.zzhQ());
        this.zzGK.put("app", context.getApplicationContext() != null ? context.getApplicationContext().getPackageName() : context.getPackageName());
        Map<String, String> map2 = this.zzGK;
        com.google.android.gms.ads.internal.zzbs.zzbz();
        map2.put("is_lite_sdk", zzagz.zzO(context) ? "1" : "0");
        Future<zzacb> futureZzn = com.google.android.gms.ads.internal.zzbs.zzbI().zzn(this.mContext);
        try {
            futureZzn.get();
            this.zzGK.put("network_coarse", Integer.toString(futureZzn.get().zzVS));
            this.zzGK.put("network_fine", Integer.toString(futureZzn.get().zzVT));
        } catch (Exception e) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "CsiConfiguration.CsiConfiguration");
        }
    }

    final Context getContext() {
        return this.mContext;
    }

    final String zzck() {
        return this.zzwH;
    }

    final boolean zzdL() {
        return this.zzGI;
    }

    final String zzdM() {
        return this.zzGJ;
    }

    final Map<String, String> zzdN() {
        return this.zzGK;
    }
}
