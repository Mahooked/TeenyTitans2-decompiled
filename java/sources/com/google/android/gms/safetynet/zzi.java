package com.google.android.gms.safetynet;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzcsn;

/* JADX INFO: loaded from: classes.dex */
final class zzi extends Api.zza<zzcsn, Api.ApiOptions.NoOptions> {
    zzi() {
    }

    @Override // com.google.android.gms.common.api.Api.zza
    public final /* synthetic */ Api.zze zza(Context context, Looper looper, zzq zzqVar, Api.ApiOptions.NoOptions noOptions, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        return new zzcsn(context, looper, zzqVar, connectionCallbacks, onConnectionFailedListener);
    }
}
