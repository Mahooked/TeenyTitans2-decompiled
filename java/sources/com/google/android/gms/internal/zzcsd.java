package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
final class zzcsd extends zzcsa.zzf {
    private /* synthetic */ String zzbBP;
    private /* synthetic */ String zzbBR;
    private /* synthetic */ int[] zzbBS;
    private /* synthetic */ int zzbBT;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzcsd(GoogleApiClient googleApiClient, int[] iArr, int i, String str, String str2) {
        super(googleApiClient);
        this.zzbBS = iArr;
        this.zzbBT = i;
        this.zzbBR = str;
        this.zzbBP = str2;
    }

    @Override // com.google.android.gms.internal.zzbay
    protected final /* synthetic */ void zza(Api.zzb zzbVar) throws RemoteException {
        zzcsn zzcsnVar = (zzcsn) zzbVar;
        ArrayList arrayList = new ArrayList();
        for (int i : this.zzbBS) {
            arrayList.add(Integer.valueOf(i));
        }
        zzcsnVar.zza(this.zzbBW, arrayList, this.zzbBT, this.zzbBR, this.zzbBP);
    }
}
