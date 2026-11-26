package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import java.util.Map;
import java.util.concurrent.CountDownLatch;

/* JADX INFO: loaded from: classes.dex */
final class zzau implements zzrd {
    private /* synthetic */ CountDownLatch zzsT;

    zzau(CountDownLatch countDownLatch) {
        this.zzsT = countDownLatch;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        this.zzsT.countDown();
        zzakaVar.getView().setVisibility(0);
    }
}
