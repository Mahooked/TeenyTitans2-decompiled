package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzcsh extends zzcsa.zze {
    private /* synthetic */ String zzbBU;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzcsh(zzcsa zzcsaVar, GoogleApiClient googleApiClient, String str) {
        super(googleApiClient);
        this.zzbBU = str;
    }

    @Override // com.google.android.gms.internal.zzbay
    protected final /* synthetic */ void zza(Api.zzb zzbVar) throws RemoteException {
        ((zzcry) ((zzcsn) zzbVar).zzrf()).zza(this.zzbBW, this.zzbBU);
    }
}
