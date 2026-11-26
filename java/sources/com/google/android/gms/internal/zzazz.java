package com.google.android.gms.internal;

import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzazz extends zzbay<Status, zzbab> {
    private final zzazu zzazM;

    zzazz(zzazu zzazuVar, GoogleApiClient googleApiClient) {
        super(zzazn.API, googleApiClient);
        this.zzazM = zzazuVar;
    }

    @Override // com.google.android.gms.internal.zzbay, com.google.android.gms.internal.zzbaz
    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Status) obj);
    }

    @Override // com.google.android.gms.internal.zzbay
    protected final /* synthetic */ void zza(Api.zzb zzbVar) throws RemoteException {
        zzbab zzbabVar = (zzbab) zzbVar;
        zzbaa zzbaaVar = new zzbaa(this);
        try {
            zzazu zzazuVar = this.zzazM;
            if (zzazuVar.zzazu != null && zzazuVar.zzazB.zzctX.length == 0) {
                zzazuVar.zzazB.zzctX = zzazuVar.zzazu.zzoU();
            }
            if (zzazuVar.zzazL != null && zzazuVar.zzazB.zzcue.length == 0) {
                zzazuVar.zzazB.zzcue = zzazuVar.zzazL.zzoU();
            }
            zzazuVar.zzazF = adp.zzc(zzazuVar.zzazB);
            ((zzbaf) zzbabVar.zzrf()).zza(zzbaaVar, this.zzazM);
        } catch (RuntimeException e) {
            Log.e("ClearcutLoggerApiImpl", "derived ClearcutLogger.MessageProducer ", e);
            zzr(new Status(10, "MessageProducer"));
        }
    }

    @Override // com.google.android.gms.internal.zzbbe
    protected final /* synthetic */ Result zzb(Status status) {
        return status;
    }
}
