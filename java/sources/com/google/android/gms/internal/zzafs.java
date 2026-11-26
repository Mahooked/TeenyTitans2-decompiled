package com.google.android.gms.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafs extends Handler {
    public zzafs(Looper looper) {
        super(looper);
    }

    @Override // android.os.Handler
    public final void handleMessage(Message message) {
        try {
            super.handleMessage(message);
        } catch (Exception e) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "AdMobHandler.handleMessage");
        }
    }
}
