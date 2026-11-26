package com.google.android.gms.dynamic;

import android.app.Activity;
import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
final class zzc implements zzi {
    private /* synthetic */ Activity val$activity;
    private /* synthetic */ zza zzaSv;
    private /* synthetic */ Bundle zzaSw;
    private /* synthetic */ Bundle zzxV;

    zzc(zza zzaVar, Activity activity, Bundle bundle, Bundle bundle2) {
        this.zzaSv = zzaVar;
        this.val$activity = activity;
        this.zzaSw = bundle;
        this.zzxV = bundle2;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final int getState() {
        return 0;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzaSv.zzaSr.onInflate(this.val$activity, this.zzaSw, this.zzxV);
    }
}
