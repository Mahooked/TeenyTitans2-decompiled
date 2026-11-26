package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzjg extends zziz.zza<zzacy> {
    private /* synthetic */ zzuq zzAH;
    private /* synthetic */ zziz zzAI;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzjg(zziz zzizVar, Context context, zzuq zzuqVar) {
        super();
        this.zzAI = zzizVar;
        this.zztF = context;
        this.zzAH = zzuqVar;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzacy zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.createRewardedVideoAd(com.google.android.gms.dynamic.zzn.zzw(this.zztF), this.zzAH, 11020000);
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzacy zzdo() throws RemoteException {
        zzacy zzacyVarZza = this.zzAI.zzAE.zza(this.zztF, this.zzAH);
        if (zzacyVarZza != null) {
            return zzacyVarZza;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.zztF, "rewarded_video");
        return new zzlr();
    }
}
