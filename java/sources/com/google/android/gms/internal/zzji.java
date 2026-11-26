package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzji {
    private static zzji zzAL;
    private static final Object zzuF = new Object();
    private final zzaiy zzAM = new zzaiy();
    private final zziz zzAN = new zziz(new zziq(), new zzip(), new zzli(), new zzqc(), new zzadh(), new zzww());

    static {
        zzji zzjiVar = new zzji();
        synchronized (zzuF) {
            zzAL = zzjiVar;
        }
    }

    protected zzji() {
    }

    private static zzji zzdr() {
        zzji zzjiVar;
        synchronized (zzuF) {
            zzjiVar = zzAL;
        }
        return zzjiVar;
    }

    public static zzaiy zzds() {
        return zzdr().zzAM;
    }

    public static zziz zzdt() {
        return zzdr().zzAN;
    }
}
