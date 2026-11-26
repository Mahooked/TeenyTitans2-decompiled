package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zznr implements Runnable {
    private /* synthetic */ zznq zzHN;

    zznr(zznq zznqVar) {
        this.zzHN = zznqVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzHN.zzHM != null) {
            this.zzHN.zzHM.zzev();
            this.zzHN.zzHM.zzet();
        }
        zznq.zza(this.zzHN, null);
    }
}
