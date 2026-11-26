package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdw extends zzec {
    public zzdw(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 51);
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        synchronized (this.zzro) {
            zzda zzdaVar = new zzda((String) this.zzrx.invoke(null, new Object[0]));
            this.zzro.zzbJ = zzdaVar.zzqB;
            this.zzro.zzbK = zzdaVar.zzqC;
        }
    }
}
