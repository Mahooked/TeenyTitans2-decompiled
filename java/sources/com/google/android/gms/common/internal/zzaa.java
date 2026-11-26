package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzaa implements zzf {
    private /* synthetic */ GoogleApiClient.ConnectionCallbacks zzaHC;

    zzaa(GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        this.zzaHC = connectionCallbacks;
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnected(@Nullable Bundle bundle) {
        this.zzaHC.onConnected(bundle);
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnectionSuspended(int i) {
        this.zzaHC.onConnectionSuspended(i);
    }
}
