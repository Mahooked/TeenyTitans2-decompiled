package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbey<A extends Api.zzb, L> {
    private final zzbdy<L> zzaEN;

    protected zzbey(zzbdy<L> zzbdyVar) {
        this.zzaEN = zzbdyVar;
    }

    protected abstract void zzc(A a, TaskCompletionSource<Void> taskCompletionSource) throws RemoteException;

    public final zzbdy<L> zzqG() {
        return this.zzaEN;
    }
}
