package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzhw implements com.google.android.gms.common.internal.zzg {
    private /* synthetic */ zzhs zzzs;

    zzhw(zzhs zzhsVar) {
        this.zzzs = zzhsVar;
    }

    @Override // com.google.android.gms.common.internal.zzg
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        synchronized (this.zzzs.mLock) {
            this.zzzs.zzzr = null;
            if (this.zzzs.zzzq != null) {
                zzhs.zza(this.zzzs, (zzhz) null);
            }
            this.zzzs.mLock.notifyAll();
        }
    }
}
