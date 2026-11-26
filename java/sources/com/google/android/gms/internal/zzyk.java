package com.google.android.gms.internal;

import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzyk implements zzahz<com.google.android.gms.ads.internal.js.zza> {
    private /* synthetic */ zzyh zzRt;

    zzyk(zzyh zzyhVar) {
        this.zzRt = zzyhVar;
    }

    @Override // com.google.android.gms.internal.zzahz
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zza zzaVar) {
        com.google.android.gms.ads.internal.zzbb zzbbVar = (com.google.android.gms.ads.internal.zzbb) new WeakReference(this.zzRt.zzRm).get();
        zzaVar.zza(zzbbVar, zzbbVar, zzbbVar, zzbbVar, false, null, null, null);
    }
}
