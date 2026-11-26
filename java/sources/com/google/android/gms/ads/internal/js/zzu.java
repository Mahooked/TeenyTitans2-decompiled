package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajs;

/* JADX INFO: loaded from: classes.dex */
final class zzu implements zzajs<zza> {
    private /* synthetic */ zzl zzLh;
    private /* synthetic */ zzac zzLo;

    zzu(zzl zzlVar, zzac zzacVar) {
        this.zzLh = zzlVar;
        this.zzLo = zzacVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(zza zzaVar) {
        synchronized (this.zzLh.mLock) {
            this.zzLh.zzLe = 0;
            if (this.zzLh.zzLd != null && this.zzLo != this.zzLh.zzLd) {
                zzafr.v("New JS engine is loaded, marking previous one as destroyable.");
                this.zzLh.zzLd.zzfc();
            }
            this.zzLh.zzLd = this.zzLo;
        }
    }
}
