package com.google.android.gms.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
final class zzbdx extends Handler {
    private /* synthetic */ zzbdw zzaEO;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzbdx(zzbdw zzbdwVar, Looper looper) {
        super(looper);
        this.zzaEO = zzbdwVar;
    }

    @Override // android.os.Handler
    public final void handleMessage(Message message) {
        zzbo.zzaf(message.what == 1);
        this.zzaEO.zzb((zzbdz) message.obj);
    }
}
