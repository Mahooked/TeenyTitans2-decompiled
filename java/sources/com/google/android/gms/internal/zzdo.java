package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdo extends zzec {
    private long startTime;

    public zzdo(zzdb zzdbVar, String str, String str2, zzax zzaxVar, long j, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 25);
        this.startTime = j;
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        long jLongValue = ((Long) this.zzrx.invoke(null, new Object[0])).longValue();
        synchronized (this.zzro) {
            this.zzro.zzbY = Long.valueOf(jLongValue);
            if (this.startTime != 0) {
                this.zzro.zzbn = Long.valueOf(jLongValue - this.startTime);
                this.zzro.zzbs = Long.valueOf(this.startTime);
            }
        }
    }
}
