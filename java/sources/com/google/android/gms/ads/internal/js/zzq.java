package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzq implements zzrd {
    private /* synthetic */ zza zzLi;
    private /* synthetic */ zzm zzLj;

    zzq(zzm zzmVar, zza zzaVar) {
        this.zzLj = zzmVar;
        this.zzLi = zzaVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        synchronized (this.zzLj.zzLh.mLock) {
            if (this.zzLj.zzLg.getStatus() == -1 || this.zzLj.zzLg.getStatus() == 1) {
                return;
            }
            this.zzLj.zzLh.zzLe = 0;
            this.zzLj.zzLh.zzLb.zzc(this.zzLi);
            this.zzLj.zzLg.zzf(this.zzLi);
            this.zzLj.zzLh.zzLd = this.zzLj.zzLg;
            zzafr.v("Successfully loaded JS Engine.");
        }
    }
}
