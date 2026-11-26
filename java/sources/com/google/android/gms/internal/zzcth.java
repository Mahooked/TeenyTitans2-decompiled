package com.google.android.gms.internal;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzcth extends Api.zza<zzctu, zzctl> {
    zzcth() {
    }

    @Override // com.google.android.gms.common.api.Api.zza
    public final /* synthetic */ Api.zze zza(Context context, Looper looper, com.google.android.gms.common.internal.zzq zzqVar, zzctl zzctlVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        zzctl zzctlVar2 = zzctlVar;
        return new zzctu(context, looper, true, zzqVar, zzctlVar2 == null ? zzctl.zzbCM : zzctlVar2, connectionCallbacks, onConnectionFailedListener);
    }
}
