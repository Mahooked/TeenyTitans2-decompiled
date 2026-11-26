package com.google.android.gms.ads.internal.js;

import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;

/* JADX INFO: loaded from: classes.dex */
final class zzs implements Runnable {
    final /* synthetic */ zza zzLi;
    private /* synthetic */ zzm zzLj;

    zzs(zzm zzmVar, zza zzaVar) {
        this.zzLj = zzmVar;
        this.zzLi = zzaVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzLj.zzLh.mLock) {
            if (this.zzLj.zzLg.getStatus() == -1 || this.zzLj.zzLg.getStatus() == 1) {
                return;
            }
            this.zzLj.zzLg.reject();
            zzbs.zzbz();
            zzagz.runOnUiThread(new zzt(this));
            zzafr.v("Could not receive loaded message in a timely manner. Rejecting.");
        }
    }
}
