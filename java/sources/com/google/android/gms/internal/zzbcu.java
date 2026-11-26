package com.google.android.gms.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

/* JADX INFO: loaded from: classes.dex */
final class zzbcu extends Handler {
    private /* synthetic */ zzbcp zzaDN;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzbcu(zzbcp zzbcpVar, Looper looper) {
        super(looper);
        this.zzaDN = zzbcpVar;
    }

    @Override // android.os.Handler
    public final void handleMessage(Message message) {
        switch (message.what) {
            case 1:
                this.zzaDN.zzqd();
                break;
            case 2:
                this.zzaDN.resume();
                break;
            default:
                Log.w("GoogleApiClientImpl", new StringBuilder(31).append("Unknown message id: ").append(message.what).toString());
                break;
        }
    }
}
