package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzjb extends zziz.zza<zzjz> {
    private /* synthetic */ zziv zzAG;
    private /* synthetic */ zziz zzAI;
    private /* synthetic */ String zztD;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzjb(zziz zzizVar, Context context, zziv zzivVar, String str) {
        super();
        this.zzAI = zzizVar;
        this.zztF = context;
        this.zzAG = zzivVar;
        this.zztD = str;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzjz zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.createSearchAdManager(com.google.android.gms.dynamic.zzn.zzw(this.zztF), this.zzAG, this.zztD, 11020000);
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzjz zzdo() throws RemoteException {
        zzjz zzjzVarZza = this.zzAI.zzAA.zza(this.zztF, this.zzAG, this.zztD, null, 3);
        if (zzjzVarZza != null) {
            return zzjzVarZza;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.zztF, "search");
        return new zzln();
    }
}
