package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdx extends zzec {
    private static volatile Long zzrt = null;
    private static final Object zzrl = new Object();

    public zzdx(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 33);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (zzrt == null) {
            synchronized (zzrl) {
                if (zzrt == null) {
                    zzrt = (Long) this.zzrx.invoke(null, new Object[0]);
                }
            }
        }
        synchronized (this.zzro) {
            this.zzro.zzbw = zzrt;
        }
    }
}
