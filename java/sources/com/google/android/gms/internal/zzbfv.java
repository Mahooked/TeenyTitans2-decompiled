package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
abstract class zzbfv extends zzbfu<Status> {
    public zzbfv(GoogleApiClient googleApiClient) {
        super(googleApiClient);
    }

    @Override // com.google.android.gms.internal.zzbbe
    public final /* synthetic */ Result zzb(Status status) {
        return status;
    }
}
