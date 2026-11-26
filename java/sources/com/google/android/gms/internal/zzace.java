package com.google.android.gms.internal;

import android.content.Context;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzace implements Callable<zzacb> {
    private /* synthetic */ zzacd zzWf;
    private /* synthetic */ Context zztF;

    zzace(zzacd zzacdVar, Context context) {
        this.zzWf = zzacdVar;
        this.zztF = context;
    }

    /* JADX WARN: Code duplicated, block: B:14:0x0066  */
    @Override // java.util.concurrent.Callable
    public final /* synthetic */ zzacb call() throws Exception {
        zzacb zzacbVarZzgM;
        zzacf zzacfVar = (zzacf) this.zzWf.zzWe.get(this.zztF);
        if (zzacfVar != null) {
            if (zzacfVar.zzWg + ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEu)).longValue() < com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis()) {
                zzacbVarZzgM = new zzacc(this.zztF).zzgM();
            } else {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEt)).booleanValue()) {
                    zzacbVarZzgM = new zzacc(this.zztF, zzacfVar.zzWh).zzgM();
                } else {
                    zzacbVarZzgM = new zzacc(this.zztF).zzgM();
                }
            }
        } else {
            zzacbVarZzgM = new zzacc(this.zztF).zzgM();
        }
        this.zzWf.zzWe.put(this.zztF, new zzacf(this.zzWf, zzacbVarZzgM));
        return zzacbVarZzgM;
    }
}
