package com.google.android.gms.dynamic;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements zzi {
    private /* synthetic */ zza zzaSv;

    zzg(zza zzaVar) {
        this.zzaSv = zzaVar;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final int getState() {
        return 4;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzaSv.zzaSr.onStart();
    }
}
