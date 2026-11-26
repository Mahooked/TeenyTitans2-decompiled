package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
final class zzcsc extends zzcsa.zzf {
    private /* synthetic */ String zzbBP;
    private /* synthetic */ List zzbBQ;
    private /* synthetic */ String zzbBR;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzcsc(zzcsa zzcsaVar, GoogleApiClient googleApiClient, List list, String str, String str2) {
        super(googleApiClient);
        this.zzbBQ = list;
        this.zzbBR = str;
        this.zzbBP = str2;
    }

    @Override // com.google.android.gms.internal.zzbay
    protected final /* synthetic */ void zza(Api.zzb zzbVar) throws RemoteException {
        ((zzcsn) zzbVar).zza(this.zzbBW, this.zzbBQ, 2, this.zzbBR, this.zzbBP);
    }
}
