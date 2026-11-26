package com.google.android.gms.internal;

import android.content.Context;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzmp implements Callable<Void> {
    private /* synthetic */ Context zztF;

    zzmp(Context context) {
        this.zztF = context;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ Void call() throws Exception {
        com.google.android.gms.ads.internal.zzbs.zzbL().initialize(this.zztF);
        return null;
    }
}
