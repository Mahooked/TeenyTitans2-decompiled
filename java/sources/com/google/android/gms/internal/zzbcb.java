package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbcb extends zzbcy {
    private /* synthetic */ zzbca zzaDb;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzbcb(zzbca zzbcaVar, zzbcw zzbcwVar) {
        super(zzbcwVar);
        this.zzaDb = zzbcaVar;
    }

    @Override // com.google.android.gms.internal.zzbcy
    public final void zzpV() {
        this.zzaDb.onConnectionSuspended(1);
    }
}
