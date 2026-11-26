package com.google.android.gms.internal;

import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzbbt {
    private final Map<zzbbe<?>, Boolean> zzaCR = Collections.synchronizedMap(new WeakHashMap());
    private final Map<TaskCompletionSource<?>, Boolean> zzaCS = Collections.synchronizedMap(new WeakHashMap());

    private final void zza(boolean z, Status status) {
        HashMap map;
        HashMap map2;
        synchronized (this.zzaCR) {
            map = new HashMap(this.zzaCR);
        }
        synchronized (this.zzaCS) {
            map2 = new HashMap(this.zzaCS);
        }
        for (Map.Entry entry : map.entrySet()) {
            if (z || ((Boolean) entry.getValue()).booleanValue()) {
                ((zzbbe) entry.getKey()).zzs(status);
            }
        }
        for (Map.Entry entry2 : map2.entrySet()) {
            if (z || ((Boolean) entry2.getValue()).booleanValue()) {
                ((TaskCompletionSource) entry2.getKey()).trySetException(new ApiException(status));
            }
        }
    }

    final void zza(zzbbe<? extends Result> zzbbeVar, boolean z) {
        this.zzaCR.put(zzbbeVar, Boolean.valueOf(z));
        zzbbeVar.zza(new zzbbu(this, zzbbeVar));
    }

    final <TResult> void zza(TaskCompletionSource<TResult> taskCompletionSource, boolean z) {
        this.zzaCS.put(taskCompletionSource, Boolean.valueOf(z));
        taskCompletionSource.getTask().addOnCompleteListener(new zzbbv(this, taskCompletionSource));
    }

    final boolean zzpO() {
        return (this.zzaCR.isEmpty() && this.zzaCS.isEmpty()) ? false : true;
    }

    public final void zzpP() {
        zza(false, zzbdb.zzaEc);
    }

    public final void zzpQ() {
        zza(true, zzbev.zzaFj);
    }
}
