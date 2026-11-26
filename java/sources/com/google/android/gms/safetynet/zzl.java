package com.google.android.gms.safetynet;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.internal.zzbeq;
import com.google.android.gms.internal.zzcry;
import com.google.android.gms.internal.zzcsn;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
final class zzl extends zzbeq<zzcsn, Void> {
    zzl(SafetyNetClient safetyNetClient) {
    }

    @Override // com.google.android.gms.internal.zzbeq
    protected final /* synthetic */ void zza(Api.zzb zzbVar, TaskCompletionSource<Void> taskCompletionSource) throws RemoteException {
        ((zzcry) ((zzcsn) zzbVar).zzrf()).zzAj();
    }
}
