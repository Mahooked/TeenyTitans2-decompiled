package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zztu {
    private static zzahz<com.google.android.gms.ads.internal.js.zza> zzLE = new zztv();
    private static zzahz<com.google.android.gms.ads.internal.js.zza> zzLF = new zztw();
    private final com.google.android.gms.ads.internal.js.zzl zzLG;

    public zztu(Context context, zzaje zzajeVar, String str) {
        this.zzLG = new com.google.android.gms.ads.internal.js.zzl(context, zzajeVar, str, zzLE, zzLF);
    }

    public final com.google.android.gms.ads.internal.js.zzl zzff() {
        return this.zzLG;
    }
}
