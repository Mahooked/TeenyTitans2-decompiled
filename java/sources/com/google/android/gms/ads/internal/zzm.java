package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaff;

/* JADX INFO: loaded from: classes.dex */
final class zzm implements Runnable {
    private /* synthetic */ zzl zzte;

    zzm(zzl zzlVar) {
        this.zzte = zzlVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzte.zztb.zzb(new zzaff(this.zzte.zzsW, null, null, null, null, null, null, null));
    }
}
