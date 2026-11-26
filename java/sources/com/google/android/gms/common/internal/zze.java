package com.google.android.gms.common.internal;

import android.app.PendingIntent;
import android.os.Bundle;
import android.os.IInterface;
import android.support.annotation.BinderThread;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
abstract class zze extends zzi<Boolean> {
    private int statusCode;
    private Bundle zzaHd;
    private /* synthetic */ zzd zzaHe;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    @BinderThread
    protected zze(zzd zzdVar, int i, Bundle bundle) {
        super(zzdVar, true);
        this.zzaHe = zzdVar;
        this.statusCode = i;
        this.zzaHd = bundle;
    }

    protected abstract void zzj(ConnectionResult connectionResult);

    protected abstract boolean zzrj();

    @Override // com.google.android.gms.common.internal.zzi
    protected final /* synthetic */ void zzs(Boolean bool) {
        if (bool == null) {
            this.zzaHe.zza(1, (IInterface) null);
            return;
        }
        switch (this.statusCode) {
            case 0:
                if (zzrj()) {
                    return;
                }
                this.zzaHe.zza(1, (IInterface) null);
                zzj(new ConnectionResult(8, null));
                return;
            case 10:
                this.zzaHe.zza(1, (IInterface) null);
                throw new IllegalStateException("A fatal developer error has occurred. Check the logs for further information.");
            default:
                this.zzaHe.zza(1, (IInterface) null);
                zzj(new ConnectionResult(this.statusCode, this.zzaHd != null ? (PendingIntent) this.zzaHd.getParcelable("pendingIntent") : null));
                return;
        }
    }
}
