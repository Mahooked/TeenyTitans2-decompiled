package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzbcm implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {
    private /* synthetic */ zzbcd zzaDp;

    private zzbcm(zzbcd zzbcdVar) {
        this.zzaDp = zzbcdVar;
    }

    /* synthetic */ zzbcm(zzbcd zzbcdVar, zzbce zzbceVar) {
        this(zzbcdVar);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(Bundle bundle) {
        this.zzaDp.zzaDh.zza(new zzbck(this.zzaDp));
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzaDp.zzaCv.lock();
        try {
            if (this.zzaDp.zzd(connectionResult)) {
                this.zzaDp.zzpZ();
                this.zzaDp.zzpX();
            } else {
                this.zzaDp.zze(connectionResult);
            }
        } finally {
            this.zzaDp.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
    }
}
