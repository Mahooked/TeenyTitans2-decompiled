package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
public final class zzcaf {
    private static zzcaf zzaXH;
    private final zzcaa zzaXI = new zzcaa();
    private final zzcab zzaXJ = new zzcab();

    static {
        zzcaf zzcafVar = new zzcaf();
        synchronized (zzcaf.class) {
            zzaXH = zzcafVar;
        }
    }

    private zzcaf() {
    }

    private static zzcaf zzua() {
        zzcaf zzcafVar;
        synchronized (zzcaf.class) {
            zzcafVar = zzaXH;
        }
        return zzcafVar;
    }

    public static zzcaa zzub() {
        return zzua().zzaXI;
    }

    public static zzcab zzuc() {
        return zzua().zzaXJ;
    }
}
