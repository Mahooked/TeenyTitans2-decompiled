package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import java.util.Map;
import java.util.concurrent.CountDownLatch;

/* JADX INFO: loaded from: classes.dex */
final class zzav implements zzrd {
    private /* synthetic */ CountDownLatch zzsT;

    zzav(CountDownLatch countDownLatch) {
        this.zzsT = countDownLatch;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzafr.zzaT("Adapter returned an ad, but assets substitution failed");
        this.zzsT.countDown();
        zzakaVar.destroy();
    }
}
