package com.google.android.gms.dynamic;

import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
final class zzd implements zzi {
    private /* synthetic */ zza zzaSv;
    private /* synthetic */ Bundle zzxV;

    zzd(zza zzaVar, Bundle bundle) {
        this.zzaSv = zzaVar;
        this.zzxV = bundle;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final int getState() {
        return 1;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzaSv.zzaSr.onCreate(this.zzxV);
    }
}
