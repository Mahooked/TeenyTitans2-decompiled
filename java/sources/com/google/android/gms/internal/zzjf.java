package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import android.widget.FrameLayout;

/* JADX INFO: loaded from: classes.dex */
final class zzjf extends zziz.zza<zzow> {
    private /* synthetic */ zziz zzAI;
    private /* synthetic */ FrameLayout zzAJ;
    private /* synthetic */ FrameLayout zzAK;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzjf(zziz zzizVar, FrameLayout frameLayout, FrameLayout frameLayout2, Context context) {
        super();
        this.zzAI = zzizVar;
        this.zzAJ = frameLayout;
        this.zzAK = frameLayout2;
        this.zztF = context;
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzow zza(zzkh zzkhVar) throws RemoteException {
        return zzkhVar.createNativeAdViewDelegate(com.google.android.gms.dynamic.zzn.zzw(this.zzAJ), com.google.android.gms.dynamic.zzn.zzw(this.zzAK));
    }

    @Override // com.google.android.gms.internal.zziz.zza
    public final /* synthetic */ zzow zzdo() throws RemoteException {
        zzow zzowVarZzb = this.zzAI.zzAD.zzb(this.zztF, this.zzAJ, this.zzAK);
        if (zzowVarZzb != null) {
            return zzowVarZzb;
        }
        zziz zzizVar = this.zzAI;
        zziz.zzb(this.zztF, "native_ad_view_delegate");
        return new zzlq();
    }
}
