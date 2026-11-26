package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
public final class zzbfr implements zzbfq {
    @Override // com.google.android.gms.internal.zzbfq
    public final PendingResult<Status> zzd(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzbfs(this, googleApiClient));
    }
}
