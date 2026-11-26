package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzco implements Runnable {
    private /* synthetic */ zzcn zzpO;

    zzco(zzcn zzcnVar) {
        this.zzpO = zzcnVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzpO.zzpM != null) {
            return;
        }
        synchronized (zzcn.zzpK) {
            if (this.zzpO.zzpM != null) {
                return;
            }
            boolean zBooleanValue = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEM)).booleanValue();
            if (zBooleanValue) {
                try {
                    zzcn.zzpL = new zzazn(this.zzpO.zzpJ.zzqD, "ADSHIELD", null);
                } catch (Throwable th) {
                    zBooleanValue = false;
                }
            }
            this.zzpO.zzpM = Boolean.valueOf(zBooleanValue);
            zzcn.zzpK.open();
        }
    }
}
