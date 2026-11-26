package com.google.android.gms.internal;

import android.content.SharedPreferences;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzme<T> {
    private final int zzBM;
    private final String zzBN;
    private final T zzBO;

    private zzme(int i, String str, T t) {
        this.zzBM = i;
        this.zzBN = str;
        this.zzBO = t;
        com.google.android.gms.ads.internal.zzbs.zzbK().zza(this);
    }

    /* synthetic */ zzme(int i, String str, Object obj, zzmf zzmfVar) {
        this(i, str, obj);
    }

    public static zzme<String> zza(int i, String str) {
        zzme<String> zzmeVarZza = zza(i, str, (String) null);
        com.google.android.gms.ads.internal.zzbs.zzbK().zzb(zzmeVarZza);
        return zzmeVarZza;
    }

    public static zzme<Float> zza(int i, String str, float f) {
        return new zzmi(i, str, Float.valueOf(0.0f));
    }

    public static zzme<Integer> zza(int i, String str, int i2) {
        return new zzmg(i, str, Integer.valueOf(i2));
    }

    public static zzme<Long> zza(int i, String str, long j) {
        return new zzmh(i, str, Long.valueOf(j));
    }

    public static zzme<Boolean> zza(int i, String str, Boolean bool) {
        return new zzmf(i, str, bool);
    }

    public static zzme<String> zza(int i, String str, String str2) {
        return new zzmj(i, str, str2);
    }

    public static zzme<String> zzb(int i, String str) {
        zzme<String> zzmeVarZza = zza(i, str, (String) null);
        com.google.android.gms.ads.internal.zzbs.zzbK().zzc(zzmeVarZza);
        return zzmeVarZza;
    }

    public final String getKey() {
        return this.zzBN;
    }

    public final int getSource() {
        return this.zzBM;
    }

    protected abstract T zza(SharedPreferences sharedPreferences);

    public abstract void zza(SharedPreferences.Editor editor, T t);

    protected abstract T zzb(JSONObject jSONObject);

    public final T zzdI() {
        return this.zzBO;
    }
}
