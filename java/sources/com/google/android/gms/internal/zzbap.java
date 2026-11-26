package com.google.android.gms.internal;

import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
public final class zzbap extends zzban {
    private zzbee<Api.zzb, ?> zzaBu;
    private zzbey<Api.zzb, ?> zzaBv;

    public zzbap(zzbef zzbefVar, TaskCompletionSource<Void> taskCompletionSource) {
        super(3, taskCompletionSource);
        this.zzaBu = zzbefVar.zzaBu;
        this.zzaBv = zzbefVar.zzaBv;
    }

    @Override // com.google.android.gms.internal.zzban, com.google.android.gms.internal.zzbam
    public final /* bridge */ /* synthetic */ void zza(@NonNull zzbbt zzbbtVar, boolean z) {
    }

    @Override // com.google.android.gms.internal.zzban
    public final void zzb(zzbdd<?> zzbddVar) throws RemoteException {
        this.zzaBu.zzb(zzbddVar.zzpJ(), this.zzalE);
        if (this.zzaBu.zzqG() != null) {
            zzbddVar.zzqs().put(this.zzaBu.zzqG(), new zzbef(this.zzaBu, this.zzaBv));
        }
    }

    @Override // com.google.android.gms.internal.zzban, com.google.android.gms.internal.zzbam
    public final /* bridge */ /* synthetic */ void zzp(@NonNull Status status) {
        super.zzp(status);
    }
}
