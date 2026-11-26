package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Scope;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@WorkerThread
public interface zzbel {
    void zzb(com.google.android.gms.common.internal.zzal zzalVar, Set<Scope> set);

    void zzh(ConnectionResult connectionResult);
}
