package com.google.android.gms.internal;

import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
public final class zzbar extends zzban {
    private zzbdy<?> zzaBy;

    public zzbar(zzbdy<?> zzbdyVar, TaskCompletionSource<Void> taskCompletionSource) {
        super(4, taskCompletionSource);
        this.zzaBy = zzbdyVar;
    }

    @Override // com.google.android.gms.internal.zzban, com.google.android.gms.internal.zzbam
    public final /* bridge */ /* synthetic */ void zza(@NonNull zzbbt zzbbtVar, boolean z) {
    }

    @Override // com.google.android.gms.internal.zzban
    public final void zzb(zzbdd<?> zzbddVar) throws RemoteException {
        zzbef zzbefVarRemove = zzbddVar.zzqs().remove(this.zzaBy);
        if (zzbefVarRemove != null) {
            zzbefVarRemove.zzaBv.zzc(zzbddVar.zzpJ(), this.zzalE);
            zzbefVarRemove.zzaBu.zzqH();
        } else {
            Log.wtf("UnregisterListenerTask", "Received call to unregister a listener without a matching registration call.", new Exception());
            this.zzalE.trySetException(new ApiException(Status.zzaBo));
        }
    }

    @Override // com.google.android.gms.internal.zzban, com.google.android.gms.internal.zzbam
    public final /* bridge */ /* synthetic */ void zzp(@NonNull Status status) {
        super.zzp(status);
    }
}
