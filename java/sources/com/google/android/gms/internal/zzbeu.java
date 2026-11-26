package com.google.android.gms.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzbeu extends Handler {
    private /* synthetic */ zzbes zzaFi;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzbeu(zzbes zzbesVar, Looper looper) {
        super(looper);
        this.zzaFi = zzbesVar;
    }

    @Override // android.os.Handler
    public final void handleMessage(Message message) {
        switch (message.what) {
            case 0:
                PendingResult<?> pendingResult = (PendingResult) message.obj;
                synchronized (this.zzaFi.zzaBW) {
                    try {
                        if (pendingResult == null) {
                            this.zzaFi.zzaFb.zzv(new Status(13, "Transform returned null"));
                        } else if (pendingResult instanceof zzbeh) {
                            this.zzaFi.zzaFb.zzv(((zzbeh) pendingResult).getStatus());
                        } else {
                            this.zzaFi.zzaFb.zza(pendingResult);
                        }
                    } catch (Throwable th) {
                        throw th;
                    }
                    break;
                }
                return;
            case 1:
                RuntimeException runtimeException = (RuntimeException) message.obj;
                String strValueOf = String.valueOf(runtimeException.getMessage());
                Log.e("TransformedResultImpl", strValueOf.length() != 0 ? "Runtime exception on the transformation worker thread: ".concat(strValueOf) : new String("Runtime exception on the transformation worker thread: "));
                throw runtimeException;
            default:
                Log.e("TransformedResultImpl", new StringBuilder(70).append("TransformationResultHandler received unknown message type: ").append(message.what).toString());
                return;
        }
    }
}
