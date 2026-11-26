package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
public final class zzbbi implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {
    private final boolean zzaCj;
    private zzbbj zzaCk;
    public final Api<?> zzayW;

    public zzbbi(Api<?> api, boolean z) {
        this.zzayW = api;
        this.zzaCj = z;
    }

    private final void zzpD() {
        zzbo.zzb(this.zzaCk, "Callbacks must be attached to a ClientConnectionHelper instance before connecting the client.");
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(@Nullable Bundle bundle) {
        zzpD();
        this.zzaCk.onConnected(bundle);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzpD();
        this.zzaCk.zza(connectionResult, this.zzayW, this.zzaCj);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
        zzpD();
        this.zzaCk.onConnectionSuspended(i);
    }

    public final void zza(zzbbj zzbbjVar) {
        this.zzaCk = zzbbjVar;
    }
}
