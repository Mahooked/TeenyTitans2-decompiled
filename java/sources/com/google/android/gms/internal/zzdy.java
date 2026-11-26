package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdy extends zzec {
    public zzdy(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 48);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        this.zzro.zzbH = 2;
        boolean zBooleanValue = ((Boolean) this.zzrx.invoke(null, this.zzpJ.getApplicationContext())).booleanValue();
        synchronized (this.zzro) {
            try {
                if (zBooleanValue) {
                    this.zzro.zzbH = 1;
                } else {
                    this.zzro.zzbH = 0;
                }
            } catch (Throwable th) {
                throw th;
            }
        }
    }
}
