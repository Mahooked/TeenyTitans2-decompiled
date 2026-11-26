package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
final class zzcsb extends zzcsa.zzb {
    private /* synthetic */ byte[] zzbBO;
    private /* synthetic */ String zzbBP;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzcsb(GoogleApiClient googleApiClient, byte[] bArr, String str) {
        super(googleApiClient);
        this.zzbBO = bArr;
        this.zzbBP = str;
    }

    @Override // com.google.android.gms.internal.zzbay
    protected final /* synthetic */ void zza(Api.zzb zzbVar) throws RemoteException {
        ((zzcsn) zzbVar).zzb(this.zzbBW, this.zzbBO, this.zzbBP);
    }
}
