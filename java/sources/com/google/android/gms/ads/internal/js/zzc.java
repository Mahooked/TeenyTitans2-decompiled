package com.google.android.gms.ads.internal.js;

import android.content.Context;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zzcu;

/* JADX INFO: loaded from: classes.dex */
final class zzc implements Runnable {
    private /* synthetic */ zzaje zzKO;
    private /* synthetic */ zzcu zzKP;
    private /* synthetic */ com.google.android.gms.ads.internal.zzv zzKQ;
    final /* synthetic */ JavascriptEngineFactory.JSEngineSettableFuture zzKR;
    private /* synthetic */ String zzKS;
    private /* synthetic */ Context zztF;

    zzc(JavascriptEngineFactory javascriptEngineFactory, Context context, zzaje zzajeVar, zzcu zzcuVar, com.google.android.gms.ads.internal.zzv zzvVar, JavascriptEngineFactory.JSEngineSettableFuture jSEngineSettableFuture, String str) {
        this.zztF = context;
        this.zzKO = zzajeVar;
        this.zzKP = zzcuVar;
        this.zzKQ = zzvVar;
        this.zzKR = jSEngineSettableFuture;
        this.zzKS = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            zze zzeVar = new zze(this.zztF, this.zzKO, this.zzKP, this.zzKQ);
            this.zzKR.mEngineReference = zzeVar;
            zzeVar.zza(new zzd(this));
            zzeVar.zzad(this.zzKS);
        } catch (zzakm e) {
            this.zzKR.zzb(e);
        }
    }
}
