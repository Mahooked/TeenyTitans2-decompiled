package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzrz implements Runnable {
    private /* synthetic */ zzry zzJN;

    zzrz(zzry zzryVar) {
        this.zzJN = zzryVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        com.google.android.gms.ads.internal.zzbs.zzbW().zzb(this.zzJN);
    }
}
