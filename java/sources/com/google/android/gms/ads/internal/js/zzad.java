package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajs;

/* JADX INFO: loaded from: classes.dex */
final class zzad implements zzajs<zza> {
    private /* synthetic */ zzy zzLw;

    zzad(zzac zzacVar, zzy zzyVar) {
        this.zzLw = zzyVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(zza zzaVar) {
        zzafr.v("Getting a new session for JS Engine.");
        this.zzLw.zzf(zzaVar.zzeY());
    }
}
