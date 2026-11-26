package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzdu extends zzec {
    private List<Long> zzrr;

    public zzdu(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 31);
        this.zzrr = null;
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        this.zzro.zzbu = -1L;
        this.zzro.zzbv = -1L;
        if (this.zzrr == null) {
            this.zzrr = (List) this.zzrx.invoke(null, this.zzpJ.getContext());
        }
        if (this.zzrr == null || this.zzrr.size() != 2) {
            return;
        }
        synchronized (this.zzro) {
            this.zzro.zzbu = Long.valueOf(this.zzrr.get(0).longValue());
            this.zzro.zzbv = Long.valueOf(this.zzrr.get(1).longValue());
        }
    }
}
