package com.google.android.gms.common.util;

import android.os.SystemClock;

/* JADX INFO: loaded from: classes.dex */
public final class zzi implements zze {
    private static zzi zzaJI = new zzi();

    private zzi() {
    }

    public static zze zzrY() {
        return zzaJI;
    }

    @Override // com.google.android.gms.common.util.zze
    public final long currentTimeMillis() {
        return System.currentTimeMillis();
    }

    @Override // com.google.android.gms.common.util.zze
    public final long elapsedRealtime() {
        return SystemClock.elapsedRealtime();
    }

    @Override // com.google.android.gms.common.util.zze
    public final long nanoTime() {
        return System.nanoTime();
    }
}
