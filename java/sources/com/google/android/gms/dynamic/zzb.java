package com.google.android.gms.dynamic;

import android.os.Bundle;
import java.util.Iterator;

/* JADX INFO: Add missing generic type declarations: [T] */
/* JADX INFO: loaded from: classes.dex */
final class zzb<T> implements zzo<T> {
    private /* synthetic */ zza zzaSv;

    zzb(zza zzaVar) {
        this.zzaSv = zzaVar;
    }

    /* JADX WARN: Incorrect types in method signature: (TT;)V */
    @Override // com.google.android.gms.dynamic.zzo
    public final void zza(LifecycleDelegate lifecycleDelegate) {
        this.zzaSv.zzaSr = lifecycleDelegate;
        Iterator it = this.zzaSv.zzaSt.iterator();
        while (it.hasNext()) {
            ((zzi) it.next()).zzb(this.zzaSv.zzaSr);
        }
        this.zzaSv.zzaSt.clear();
        zza.zza(this.zzaSv, (Bundle) null);
    }
}
