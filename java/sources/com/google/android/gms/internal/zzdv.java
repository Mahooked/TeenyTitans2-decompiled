package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdv extends zzec {
    private final StackTraceElement[] zzrs;

    public zzdv(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2, StackTraceElement[] stackTraceElementArr) {
        super(zzdbVar, str, str2, zzaxVar, i, 45);
        this.zzrs = stackTraceElementArr;
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (this.zzrs != null) {
            zzcz zzczVar = new zzcz((String) this.zzrx.invoke(null, this.zzrs));
            synchronized (this.zzro) {
                this.zzro.zzbG = zzczVar.zzqy;
                if (zzczVar.zzqz.booleanValue()) {
                    this.zzro.zzbO = Integer.valueOf(zzczVar.zzqA.booleanValue() ? 0 : 1);
                }
            }
        }
    }
}
