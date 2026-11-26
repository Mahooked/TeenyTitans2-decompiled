package com.google.android.gms.ads.internal.js;

import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;

/* JADX INFO: loaded from: classes.dex */
final class zzo implements Runnable {
    final /* synthetic */ zzn zzLk;

    zzo(zzn zznVar) {
        this.zzLk = zznVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzLk.zzLj.zzLh.mLock) {
            if (this.zzLk.zzLj.zzLg.getStatus() == -1 || this.zzLk.zzLj.zzLg.getStatus() == 1) {
                return;
            }
            this.zzLk.zzLj.zzLg.reject();
            zzbs.zzbz();
            zzagz.runOnUiThread(new zzp(this));
            zzafr.v("Could not receive loaded message in a timely manner. Rejecting.");
        }
    }
}
