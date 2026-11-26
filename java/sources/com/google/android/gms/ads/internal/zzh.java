package com.google.android.gms.ads.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzh implements Runnable {
    private /* synthetic */ zzd zzsZ;

    zzh(zzd zzdVar) {
        this.zzsZ = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzsZ.zzsO.resume();
    }
}
