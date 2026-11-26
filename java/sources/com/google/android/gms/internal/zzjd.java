package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzjd extends zziz.zza<zzju> {
    private /* synthetic */ zzuq zzAH;
    private /* synthetic */ zziz zzAI;
    private /* synthetic */ String zztD;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzjd(zziz zzizVar, Context context, String str, zzuq zzuqVar) {
        super();
        this.zzAI = zzizVar;
        this.zztF = context;
        this.zztD = str;
        this.zzAH = zzuqVar;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzju zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.createAdLoaderBuilder(com.google.android.gms.dynamic.zzn.zzw(this.zztF), this.zztD, this.zzAH, 11020000);
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzju zzdo() throws RemoteException {
        zzju zzjuVarZza = this.zzAI.zzAB.zza(this.zztF, this.zztD, this.zzAH);
        if (zzjuVarZza != null) {
            return zzjuVarZza;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.zztF, "native_ad");
        return new zzlj();
    }
}
