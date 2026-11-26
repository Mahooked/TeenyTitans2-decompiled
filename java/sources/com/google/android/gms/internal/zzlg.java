package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzlg implements Runnable {
    private /* synthetic */ zzlf zzBq;
    private /* synthetic */ Context zztF;

    zzlg(zzlf zzlfVar, Context context) {
        this.zzBq = zzlfVar;
        this.zztF = context;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzBq.getRewardedVideoAdInstance(this.zztF);
    }
}
