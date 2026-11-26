package com.google.android.gms.ads.internal;

import android.os.Bundle;
import com.google.ads.mediation.admob.AdMobAdapter;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zzmo;
import java.util.Map;
import java.util.TreeMap;

/* JADX INFO: loaded from: classes.dex */
final class zzbr {
    private final String zzvg;
    private final Map<String, String> zzvh = new TreeMap();
    private String zzvi;
    private String zzvj;

    public zzbr(String str) {
        this.zzvg = str;
    }

    public final String getQuery() {
        return this.zzvi;
    }

    public final void zza(zzir zzirVar, zzaje zzajeVar) {
        this.zzvi = zzirVar.zzzU.zzBI;
        Bundle bundle = zzirVar.zzzX != null ? zzirVar.zzzX.getBundle(AdMobAdapter.class.getName()) : null;
        if (bundle == null) {
            return;
        }
        String str = (String) zzbs.zzbL().zzd(zzmo.zzFW);
        for (String str2 : bundle.keySet()) {
            if (str.equals(str2)) {
                this.zzvj = bundle.getString(str2);
            } else if (str2.startsWith("csa_")) {
                this.zzvh.put(str2.substring(4), bundle.getString(str2));
            }
        }
        this.zzvh.put("SDKVersion", zzajeVar.zzaP);
    }

    public final String zzbr() {
        return this.zzvj;
    }

    public final String zzbs() {
        return this.zzvg;
    }

    public final Map<String, String> zzbt() {
        return this.zzvh;
    }
}
