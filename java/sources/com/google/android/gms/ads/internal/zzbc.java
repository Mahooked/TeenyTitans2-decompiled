package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;

/* JADX INFO: loaded from: classes.dex */
final class zzbc implements Runnable {
    private /* synthetic */ zzafg zzsW;
    private /* synthetic */ zzbb zzuQ;

    zzbc(zzbb zzbbVar, zzafg zzafgVar) {
        this.zzuQ = zzbbVar;
        this.zzsW = zzafgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzuQ.zzb(new zzaff(this.zzsW, null, null, null, null, null, null, null));
    }
}
