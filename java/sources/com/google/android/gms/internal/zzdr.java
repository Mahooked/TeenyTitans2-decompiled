package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdr extends zzec {
    private static volatile Long zzrp = null;
    private static final Object zzrl = new Object();

    public zzdr(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 22);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (zzrp == null) {
            synchronized (zzrl) {
                if (zzrp == null) {
                    zzrp = (Long) this.zzrx.invoke(null, new Object[0]);
                }
            }
        }
        synchronized (this.zzro) {
            this.zzro.zzbr = zzrp;
        }
    }
}
