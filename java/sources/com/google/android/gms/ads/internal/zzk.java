package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;

/* JADX INFO: loaded from: classes.dex */
final class zzk implements Runnable {
    private /* synthetic */ zzafg zzsW;
    private /* synthetic */ zzi zztb;

    zzk(zzi zziVar, zzafg zzafgVar) {
        this.zztb = zziVar;
        this.zzsW = zzafgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zztb.zzb(new zzaff(this.zzsW, null, null, null, null, null, null, null));
    }
}
