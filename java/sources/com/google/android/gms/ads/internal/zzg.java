package com.google.android.gms.ads.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements Runnable {
    private /* synthetic */ zzd zzsZ;

    zzg(zzd zzdVar) {
        this.zzsZ = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzsZ.zzsO.pause();
    }
}
