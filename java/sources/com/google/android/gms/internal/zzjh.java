package com.google.android.gms.internal;

import android.app.Activity;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzjh extends zziz.zza<zzwx> {
    private /* synthetic */ Activity val$activity;
    private /* synthetic */ zziz zzAI;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzjh(zziz zzizVar, Activity activity) {
        super();
        this.zzAI = zzizVar;
        this.val$activity = activity;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzwx zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.createAdOverlay(com.google.android.gms.dynamic.zzn.zzw(this.val$activity));
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzwx zzdo() throws RemoteException {
        zzwx zzwxVarZze = this.zzAI.zzAF.zze(this.val$activity);
        if (zzwxVarZze != null) {
            return zzwxVarZze;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.val$activity, "ad_overlay");
        return null;
    }
}
