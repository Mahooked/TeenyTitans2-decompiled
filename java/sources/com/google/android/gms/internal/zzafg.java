package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafg {
    public final int errorCode;
    public final zzaae zzUj;

    @Nullable
    public final JSONObject zzXL;
    public final zzub zzXN;
    public final long zzXR;
    public final long zzXS;
    public final zzig zzXX;
    public final zzaai zzXY;

    @Nullable
    public final zziv zzvX;

    public zzafg(zzaae zzaaeVar, zzaai zzaaiVar, zzub zzubVar, zziv zzivVar, int i, long j, long j2, JSONObject jSONObject, zzig zzigVar) {
        this.zzUj = zzaaeVar;
        this.zzXY = zzaaiVar;
        this.zzXN = zzubVar;
        this.zzvX = zzivVar;
        this.errorCode = i;
        this.zzXR = j;
        this.zzXS = j2;
        this.zzXL = jSONObject;
        this.zzXX = zzigVar;
    }

    public zzafg(zzaae zzaaeVar, zzaai zzaaiVar, zzub zzubVar, zziv zzivVar, int i, long j, long j2, JSONObject jSONObject, zzij zzijVar) {
        this.zzUj = zzaaeVar;
        this.zzXY = zzaaiVar;
        this.zzXN = zzubVar;
        this.zzvX = zzivVar;
        this.errorCode = i;
        this.zzXR = j;
        this.zzXS = j2;
        this.zzXL = jSONObject;
        this.zzXX = new zzig(zzijVar, ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGC)).booleanValue());
    }
}
