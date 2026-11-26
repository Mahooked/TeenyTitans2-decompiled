package com.google.android.gms.internal;

import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzbch extends zzbcy {
    private /* synthetic */ ConnectionResult zzaDs;
    private /* synthetic */ zzbcg zzaDt;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzbch(zzbcg zzbcgVar, zzbcw zzbcwVar, ConnectionResult connectionResult) {
        super(zzbcwVar);
        this.zzaDt = zzbcgVar;
        this.zzaDs = connectionResult;
    }

    @Override // com.google.android.gms.internal.zzbcy
    public final void zzpV() {
        this.zzaDt.zzaDp.zze(this.zzaDs);
    }
}
