package com.google.android.gms.internal;

import java.util.concurrent.Callable;

/* JADX INFO: Add missing generic type declarations: [T] */
/* JADX INFO: loaded from: classes.dex */
final class zzmn<T> implements Callable<T> {
    private /* synthetic */ zzme zzBU;
    private /* synthetic */ zzmm zzBV;

    zzmn(zzmm zzmmVar, zzme zzmeVar) {
        this.zzBV = zzmmVar;
        this.zzBU = zzmeVar;
    }

    @Override // java.util.concurrent.Callable
    public final T call() {
        return (T) this.zzBU.zza(this.zzBV.zzBT);
    }
}
