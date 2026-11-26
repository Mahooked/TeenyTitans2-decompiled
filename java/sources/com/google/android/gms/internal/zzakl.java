package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.ads.internal.zzbl;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzakl implements Callable<zzaka> {
    private /* synthetic */ zziv zzAG;
    private /* synthetic */ zzaje zzKO;
    private /* synthetic */ com.google.android.gms.ads.internal.zzv zzKQ;
    private /* synthetic */ zzcu zzLf;
    private /* synthetic */ boolean zzabL;
    private /* synthetic */ boolean zzabM;
    private /* synthetic */ zznb zzabN;
    private /* synthetic */ zzbl zzabO;
    private /* synthetic */ zzig zzabP;
    private /* synthetic */ Context zztF;

    zzakl(zzakk zzakkVar, Context context, zziv zzivVar, boolean z, boolean z2, zzcu zzcuVar, zzaje zzajeVar, zznb zznbVar, zzbl zzblVar, com.google.android.gms.ads.internal.zzv zzvVar, zzig zzigVar) {
        this.zztF = context;
        this.zzAG = zzivVar;
        this.zzabL = z;
        this.zzabM = z2;
        this.zzLf = zzcuVar;
        this.zzKO = zzajeVar;
        this.zzabN = zznbVar;
        this.zzabO = zzblVar;
        this.zzKQ = zzvVar;
        this.zzabP = zzigVar;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ zzaka call() throws Exception {
        zzakn zzaknVar = new zzakn(zzako.zzb(this.zztF, this.zzAG, this.zzabL, this.zzabM, this.zzLf, this.zzKO, this.zzabN, this.zzabO, this.zzKQ, this.zzabP));
        zzaknVar.setWebViewClient(com.google.android.gms.ads.internal.zzbs.zzbB().zzb(zzaknVar, this.zzabM));
        zzaknVar.setWebChromeClient(com.google.android.gms.ads.internal.zzbs.zzbB().zzm(zzaknVar));
        return zzaknVar;
    }
}
