package com.google.android.gms.ads.internal;

import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzf implements Callable<String> {
    private /* synthetic */ zzd zzsZ;

    zzf(zzd zzdVar) {
        this.zzsZ = zzdVar;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ String call() throws Exception {
        return this.zzsZ.zzsP.zzvS.zzB().zza(this.zzsZ.zzsP.zzqD);
    }
}
