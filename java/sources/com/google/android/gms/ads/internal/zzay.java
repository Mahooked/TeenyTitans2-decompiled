package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzagz;

/* JADX INFO: loaded from: classes.dex */
final class zzay implements Runnable {
    final /* synthetic */ Runnable zzuL;
    final /* synthetic */ zzax zzuM;

    zzay(zzax zzaxVar, Runnable runnable) {
        this.zzuM = zzaxVar;
        this.zzuL = runnable;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzbs.zzbz();
        zzagz.runOnUiThread(new zzaz(this));
    }
}
