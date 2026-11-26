package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdn extends zzec {
    private static volatile Long zzbF = null;
    private static final Object zzrl = new Object();

    public zzdn(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 44);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (zzbF == null) {
            synchronized (zzrl) {
                if (zzbF == null) {
                    zzbF = (Long) this.zzrx.invoke(null, new Object[0]);
                }
            }
        }
        synchronized (this.zzro) {
            this.zzro.zzbF = zzbF;
        }
    }
}
