package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzyl implements Runnable {
    private /* synthetic */ com.google.android.gms.ads.internal.js.zza zzRu;

    zzyl(zzyh zzyhVar, com.google.android.gms.ads.internal.js.zza zzaVar) {
        this.zzRu = zzaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzRu.destroy();
    }
}
