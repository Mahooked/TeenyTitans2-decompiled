package com.google.android.gms.internal;

import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzbbu implements PendingResult.zza {
    private /* synthetic */ zzbbe zzaCT;
    private /* synthetic */ zzbbt zzaCU;

    zzbbu(zzbbt zzbbtVar, zzbbe zzbbeVar) {
        this.zzaCU = zzbbtVar;
        this.zzaCT = zzbbeVar;
    }

    @Override // com.google.android.gms.common.api.PendingResult.zza
    public final void zzo(Status status) {
        this.zzaCU.zzaCR.remove(this.zzaCT);
    }
}
