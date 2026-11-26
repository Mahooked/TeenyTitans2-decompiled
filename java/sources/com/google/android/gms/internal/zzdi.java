package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdi extends zzec {
    public zzdi(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 3);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        synchronized (this.zzro) {
            this.zzro.zzaZ = -1L;
            this.zzro.zzaZ = (Long) this.zzrx.invoke(null, this.zzpJ.getContext());
        }
    }
}
