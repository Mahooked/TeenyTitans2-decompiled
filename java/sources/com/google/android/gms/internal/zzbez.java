package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
public class zzbez<T> {
    private String zzBN;
    private T zzBO;
    private T zzaFq = null;
    private static final Object zzuF = new Object();
    private static zzbff zzaFo = null;
    private static int zzaFp = 0;
    private static String READ_PERMISSION = "com.google.android.providers.gsf.permission.READ_GSERVICES";

    protected zzbez(String str, T t) {
        this.zzBN = str;
        this.zzBO = t;
    }

    public static zzbez<Float> zza(String str, Float f) {
        return new zzbfd(str, f);
    }

    public static zzbez<Integer> zza(String str, Integer num) {
        return new zzbfc(str, num);
    }

    public static zzbez<Long> zza(String str, Long l) {
        return new zzbfb(str, l);
    }

    public static zzbez<Boolean> zzg(String str, boolean z) {
        return new zzbfa(str, Boolean.valueOf(z));
    }

    public static zzbez<String> zzv(String str, String str2) {
        return new zzbfe(str, str2);
    }
}
