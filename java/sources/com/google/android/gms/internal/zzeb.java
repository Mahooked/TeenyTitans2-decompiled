package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzeb implements Runnable {
    private /* synthetic */ zzea zzrA;

    zzeb(zzea zzeaVar) {
        this.zzrA = zzeaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzrA.zzX();
    }
}
