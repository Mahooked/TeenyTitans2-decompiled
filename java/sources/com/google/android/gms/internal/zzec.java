package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzec implements Callable {
    private String TAG = getClass().getSimpleName();
    private String className;
    protected final zzdb zzpJ;
    private int zzrB;
    private int zzrC;
    protected final zzax zzro;
    private String zzrv;
    protected Method zzrx;

    public zzec(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        this.zzpJ = zzdbVar;
        this.className = str;
        this.zzrv = str2;
        this.zzro = zzaxVar;
        this.zzrB = i;
        this.zzrC = i2;
    }

    protected abstract void zzT() throws IllegalAccessException, InvocationTargetException;

    @Override // java.util.concurrent.Callable
    /* JADX INFO: renamed from: zzV, reason: merged with bridge method [inline-methods] */
    public Void call() throws Exception {
        try {
            long jNanoTime = System.nanoTime();
            this.zzrx = this.zzpJ.zzc(this.className, this.zzrv);
            if (this.zzrx != null) {
                zzT();
                zzcn zzcnVarZzI = this.zzpJ.zzI();
                if (zzcnVarZzI != null && this.zzrB != Integer.MIN_VALUE) {
                    zzcnVarZzI.zza(this.zzrC, this.zzrB, (System.nanoTime() - jNanoTime) / 1000);
                }
            }
        } catch (IllegalAccessException e) {
        } catch (InvocationTargetException e2) {
        }
        return null;
    }
}
