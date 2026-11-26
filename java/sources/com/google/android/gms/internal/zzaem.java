package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzaem implements GoogleApiClient.OnConnectionFailedListener {
    private /* synthetic */ zzajg zzXk;

    zzaem(zzael zzaelVar, zzajg zzajgVar) {
        this.zzXk = zzajgVar;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzaes.zzaC("Failed to connect to SafetyNet API");
        this.zzXk.zzg(null);
    }
}
