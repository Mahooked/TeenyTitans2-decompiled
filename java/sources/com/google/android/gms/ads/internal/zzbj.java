package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzir;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzbj implements Runnable {
    private /* synthetic */ WeakReference zzuX;
    private /* synthetic */ zzbi zzuY;

    zzbj(zzbi zzbiVar, WeakReference weakReference) {
        this.zzuY = zzbiVar;
        this.zzuX = weakReference;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzbi.zza(this.zzuY, false);
        zza zzaVar = (zza) this.zzuX.get();
        if (zzaVar != null) {
            zzir zzirVar = this.zzuY.zzuT;
            if (zzaVar.zzb(zzirVar)) {
                zzaVar.zza(zzirVar);
            } else {
                zzafr.zzaS("Ad is not visible. Not refreshing ad.");
                zzaVar.zzsO.zzg(zzirVar);
            }
        }
    }
}
