package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzeu;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzbp implements Callable<zzeu> {
    private /* synthetic */ zzbm zzvf;

    zzbp(zzbm zzbmVar) {
        this.zzvf = zzbmVar;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ zzeu call() throws Exception {
        return new zzeu(this.zzvf.zztW.zzaP, this.zzvf.mContext, false);
    }
}
