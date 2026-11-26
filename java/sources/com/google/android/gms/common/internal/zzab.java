package com.google.android.gms.common.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzab implements zzg {
    private /* synthetic */ GoogleApiClient.OnConnectionFailedListener zzaHD;

    zzab(GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        this.zzaHD = onConnectionFailedListener;
    }

    @Override // com.google.android.gms.common.internal.zzg
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzaHD.onConnectionFailed(connectionResult);
    }
}
