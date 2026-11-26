package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzje extends zziz.zza<zzkn> {
    private /* synthetic */ zziz zzAI;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzje(zziz zzizVar, Context context) {
        super();
        this.zzAI = zzizVar;
        this.zztF = context;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzkn zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.getMobileAdsSettingsManagerWithClientJarVersion(com.google.android.gms.dynamic.zzn.zzw(this.zztF), 11020000);
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzkn zzdo() throws RemoteException {
        zzkn zzknVarZzh = this.zzAI.zzAC.zzh(this.zztF);
        if (zzknVarZzh != null) {
            return zzknVarZzh;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.zztF, "mobile_ads_settings");
        return new zzlp();
    }
}
