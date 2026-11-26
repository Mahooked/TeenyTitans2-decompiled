package com.google.android.gms.internal;

import android.os.Bundle;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
final class zzbcr implements GoogleApiClient.ConnectionCallbacks {
    private /* synthetic */ zzbcp zzaDN;
    private /* synthetic */ AtomicReference zzaDO;
    private /* synthetic */ zzben zzaDP;

    zzbcr(zzbcp zzbcpVar, AtomicReference atomicReference, zzben zzbenVar) {
        this.zzaDN = zzbcpVar;
        this.zzaDO = atomicReference;
        this.zzaDP = zzbenVar;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(Bundle bundle) {
        this.zzaDN.zza((GoogleApiClient) this.zzaDO.get(), this.zzaDP, true);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
    }
}
