package com.google.android.gms.internal;

import android.os.DeadObjectException;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
abstract class zzban extends zzbam {
    protected final TaskCompletionSource<Void> zzalE;

    public zzban(int i, TaskCompletionSource<Void> taskCompletionSource) {
        super(i);
        this.zzalE = taskCompletionSource;
    }

    @Override // com.google.android.gms.internal.zzbam
    public void zza(@NonNull zzbbt zzbbtVar, boolean z) {
    }

    @Override // com.google.android.gms.internal.zzbam
    public final void zza(zzbdd<?> zzbddVar) throws DeadObjectException {
        try {
            zzb(zzbddVar);
        } catch (DeadObjectException e) {
            zzp(zzbam.zza(e));
            throw e;
        } catch (RemoteException e2) {
            zzp(zzbam.zza(e2));
        }
    }

    protected abstract void zzb(zzbdd<?> zzbddVar) throws RemoteException;

    @Override // com.google.android.gms.internal.zzbam
    public void zzp(@NonNull Status status) {
        this.zzalE.trySetException(new ApiException(status));
    }
}
