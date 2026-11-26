package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzsi {
    private final Context mContext;
    private final com.google.android.gms.ads.internal.zzv zzsS;
    private final zzuq zzsX;
    private final zzaje zztW;

    zzsi(Context context, zzuq zzuqVar, zzaje zzajeVar, com.google.android.gms.ads.internal.zzv zzvVar) {
        this.mContext = context;
        this.zzsX = zzuqVar;
        this.zztW = zzajeVar;
        this.zzsS = zzvVar;
    }

    public final Context getApplicationContext() {
        return this.mContext.getApplicationContext();
    }

    public final com.google.android.gms.ads.internal.zzal zzW(String str) {
        return new com.google.android.gms.ads.internal.zzal(this.mContext, new zziv(), str, this.zzsX, this.zztW, this.zzsS);
    }

    public final com.google.android.gms.ads.internal.zzal zzX(String str) {
        return new com.google.android.gms.ads.internal.zzal(this.mContext.getApplicationContext(), new zziv(), str, this.zzsX, this.zztW, this.zzsS);
    }

    public final zzsi zzeF() {
        return new zzsi(this.mContext.getApplicationContext(), this.zzsX, this.zztW, this.zzsS);
    }
}
