package com.google.android.gms.internal;

import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
final class zzto {
    boolean zzKA;
    private /* synthetic */ zztn zzKB;
    com.google.android.gms.ads.internal.zzal zzKv;

    @Nullable
    zzir zzKw;
    zzsj zzKx;
    long zzKy;
    boolean zzKz;

    zzto(zztn zztnVar, zzsi zzsiVar) {
        this.zzKB = zztnVar;
        this.zzKv = zzsiVar.zzX(zztnVar.zztV);
        this.zzKx = new zzsj();
        zzsj zzsjVar = this.zzKx;
        com.google.android.gms.ads.internal.zzal zzalVar = this.zzKv;
        zzalVar.zza(new zzsk(zzsjVar));
        zzalVar.zza(new zzss(zzsjVar));
        zzalVar.zza(new zzsu(zzsjVar));
        zzalVar.zza(new zzsw(zzsjVar));
        zzalVar.zza(new zzsy(zzsjVar));
    }

    zzto(zztn zztnVar, zzsi zzsiVar, zzir zzirVar) {
        this(zztnVar, zzsiVar);
        this.zzKw = zzirVar;
    }

    final boolean load() {
        if (this.zzKz) {
            return false;
        }
        this.zzKA = this.zzKv.zza(zztl.zzj(this.zzKw != null ? this.zzKw : this.zzKB.zzKs));
        this.zzKz = true;
        this.zzKy = com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis();
        return true;
    }
}
