package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzais;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzr implements zzrd {
    private /* synthetic */ zza zzLi;
    private /* synthetic */ zzm zzLj;
    private /* synthetic */ zzais zzLm;

    zzr(zzm zzmVar, zza zzaVar, zzais zzaisVar) {
        this.zzLj = zzmVar;
        this.zzLi = zzaVar;
        this.zzLm = zzaisVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        synchronized (this.zzLj.zzLh.mLock) {
            zzafr.zzaS("JS Engine is requesting an update");
            if (this.zzLj.zzLh.zzLe == 0) {
                zzafr.zzaS("Starting reload.");
                this.zzLj.zzLh.zzLe = 2;
                this.zzLj.zzLh.zza(this.zzLj.zzLf);
            }
            this.zzLi.zzb("/requestReload", (zzrd) this.zzLm.get());
        }
    }
}
