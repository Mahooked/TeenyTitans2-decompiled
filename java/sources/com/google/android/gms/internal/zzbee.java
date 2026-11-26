package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbee<A extends Api.zzb, L> {
    private final zzbdw<L> zzaEU;

    protected zzbee(zzbdw<L> zzbdwVar) {
        this.zzaEU = zzbdwVar;
    }

    protected abstract void zzb(A a, TaskCompletionSource<Void> taskCompletionSource) throws RemoteException;

    public final zzbdy<L> zzqG() {
        return this.zzaEU.zzqG();
    }

    public final void zzqH() {
        this.zzaEU.clear();
    }
}
