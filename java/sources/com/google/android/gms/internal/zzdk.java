package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdk extends zzec {
    private static volatile String zzrk = null;
    private static final Object zzrl = new Object();

    public zzdk(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 29);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        this.zzro.zzbt = "E";
        if (zzrk == null) {
            synchronized (zzrl) {
                if (zzrk == null) {
                    zzrk = (String) this.zzrx.invoke(null, this.zzpJ.getContext());
                }
            }
        }
        synchronized (this.zzro) {
            this.zzro.zzbt = zzbt.zza(zzrk.getBytes(), true);
        }
    }
}
