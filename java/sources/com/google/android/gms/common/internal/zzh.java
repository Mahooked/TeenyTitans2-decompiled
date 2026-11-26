package com.google.android.gms.common.internal;

import android.app.PendingIntent;
import android.os.Handler;
import android.os.IInterface;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzh extends Handler {
    private /* synthetic */ zzd zzaHe;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzh(zzd zzdVar, Looper looper) {
        super(looper);
        this.zzaHe = zzdVar;
    }

    private static void zza(Message message) {
        ((zzi) message.obj).unregister();
    }

    private static boolean zzb(Message message) {
        return message.what == 2 || message.what == 1 || message.what == 7;
    }

    @Override // android.os.Handler
    public final void handleMessage(Message message) {
        if (this.zzaHe.zzaHb.get() != message.arg1) {
            if (zzb(message)) {
                zza(message);
                return;
            }
            return;
        }
        if ((message.what == 1 || message.what == 7 || message.what == 4 || message.what == 5) && !this.zzaHe.isConnecting()) {
            zza(message);
            return;
        }
        if (message.what == 4) {
            this.zzaHe.zzaGZ = new ConnectionResult(message.arg2);
            if (this.zzaHe.zzri() && !this.zzaHe.zzaHa) {
                this.zzaHe.zza(3, (IInterface) null);
                return;
            }
            ConnectionResult connectionResult = this.zzaHe.zzaGZ != null ? this.zzaHe.zzaGZ : new ConnectionResult(8);
            this.zzaHe.zzaGQ.zzf(connectionResult);
            this.zzaHe.onConnectionFailed(connectionResult);
            return;
        }
        if (message.what == 5) {
            ConnectionResult connectionResult2 = this.zzaHe.zzaGZ != null ? this.zzaHe.zzaGZ : new ConnectionResult(8);
            this.zzaHe.zzaGQ.zzf(connectionResult2);
            this.zzaHe.onConnectionFailed(connectionResult2);
            return;
        }
        if (message.what == 3) {
            ConnectionResult connectionResult3 = new ConnectionResult(message.arg2, message.obj instanceof PendingIntent ? (PendingIntent) message.obj : null);
            this.zzaHe.zzaGQ.zzf(connectionResult3);
            this.zzaHe.onConnectionFailed(connectionResult3);
            return;
        }
        if (message.what == 6) {
            this.zzaHe.zza(5, (IInterface) null);
            if (this.zzaHe.zzaGV != null) {
                this.zzaHe.zzaGV.onConnectionSuspended(message.arg2);
            }
            this.zzaHe.onConnectionSuspended(message.arg2);
            this.zzaHe.zza(5, 1, (IInterface) null);
            return;
        }
        if (message.what == 2 && !this.zzaHe.isConnected()) {
            zza(message);
        } else if (zzb(message)) {
            ((zzi) message.obj).zzrk();
        } else {
            Log.wtf("GmsClient", new StringBuilder(45).append("Don't know how to handle message: ").append(message.what).toString(), new Exception());
        }
    }
}
