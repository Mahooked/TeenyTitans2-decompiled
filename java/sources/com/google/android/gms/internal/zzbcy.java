package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
abstract class zzbcy {
    private final zzbcw zzaDZ;

    protected zzbcy(zzbcw zzbcwVar) {
        this.zzaDZ = zzbcwVar;
    }

    public final void zzc(zzbcx zzbcxVar) {
        zzbcxVar.zzaCv.lock();
        try {
            if (zzbcxVar.zzaDV != this.zzaDZ) {
                return;
            }
            zzpV();
        } finally {
            zzbcxVar.zzaCv.unlock();
        }
    }

    protected abstract void zzpV();
}
