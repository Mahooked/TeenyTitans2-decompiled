package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdm extends zzec {
    public zzdm(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 5);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        this.zzro.zzbb = -1L;
        this.zzro.zzbc = -1L;
        int[] iArr = (int[]) this.zzrx.invoke(null, this.zzpJ.getContext());
        synchronized (this.zzro) {
            this.zzro.zzbb = Long.valueOf(iArr[0]);
            this.zzro.zzbc = Long.valueOf(iArr[1]);
        }
    }
}
