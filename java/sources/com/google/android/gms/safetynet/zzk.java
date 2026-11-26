package com.google.android.gms.safetynet;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzber;
import com.google.android.gms.internal.zzcru;
import com.google.android.gms.tasks.TaskCompletionSource;

/* JADX INFO: loaded from: classes.dex */
final class zzk extends zzcru {
    private /* synthetic */ TaskCompletionSource zzaCV;

    zzk(zzj zzjVar, TaskCompletionSource taskCompletionSource) {
        this.zzaCV = taskCompletionSource;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zzF(Status status) {
        zzber.zza(status, null, this.zzaCV);
    }
}
