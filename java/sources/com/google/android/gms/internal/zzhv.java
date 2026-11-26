package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.DeadObjectException;
import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
final class zzhv implements com.google.android.gms.common.internal.zzf {
    private /* synthetic */ zzhs zzzs;

    zzhv(zzhs zzhsVar) {
        this.zzzs = zzhsVar;
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnected(@Nullable Bundle bundle) {
        synchronized (this.zzzs.mLock) {
            try {
                if (this.zzzs.zzzq != null) {
                    this.zzzs.zzzr = this.zzzs.zzzq.zzdd();
                }
            } catch (DeadObjectException e) {
                zzafr.zzb("Unable to obtain a cache service instance.", e);
                this.zzzs.disconnect();
            }
            this.zzzs.mLock.notifyAll();
        }
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnectionSuspended(int i) {
        synchronized (this.zzzs.mLock) {
            this.zzzs.zzzr = null;
            this.zzzs.mLock.notifyAll();
        }
    }
}
