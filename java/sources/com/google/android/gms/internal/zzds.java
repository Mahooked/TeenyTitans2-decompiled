package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzds extends zzec {
    private long zzrq;

    public zzds(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 12);
        this.zzrq = -1L;
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzER)).booleanValue()) {
            this.zzro.zzbi = -1L;
            this.zzro.zzbi = (Long) this.zzrx.invoke(null, this.zzpJ.getContext());
            return;
        }
        this.zzro.zzbi = -1L;
        if (this.zzrq == -1) {
            this.zzrq = ((Long) this.zzrx.invoke(null, this.zzpJ.getContext())).longValue();
        }
        synchronized (this.zzro) {
            this.zzro.zzbi = Long.valueOf(this.zzrq);
        }
    }
}
