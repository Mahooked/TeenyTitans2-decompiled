package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zznt implements Runnable {
    private /* synthetic */ zzns zzHQ;

    zznt(zzns zznsVar) {
        this.zzHQ = zznsVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzHQ.zzHM != null) {
            this.zzHQ.zzHM.zzev();
            this.zzHQ.zzHM.zzet();
        }
        zzns.zza(this.zzHQ, null);
    }
}
