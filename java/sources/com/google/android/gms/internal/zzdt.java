package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdt extends zzec {
    private static volatile String zzaY = null;
    private static final Object zzrl = new Object();

    public zzdt(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 1);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        this.zzro.zzaY = "E";
        if (zzaY == null) {
            synchronized (zzrl) {
                if (zzaY == null) {
                    zzaY = (String) this.zzrx.invoke(null, new Object[0]);
                }
            }
        }
        synchronized (this.zzro) {
            this.zzro.zzaY = zzaY;
        }
    }
}
