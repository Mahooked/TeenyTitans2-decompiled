package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzabq implements Runnable {
    private /* synthetic */ zzabu zzUA;
    private /* synthetic */ zzabl zzUF;
    private /* synthetic */ zzaae zzUG;
    private /* synthetic */ Context zztF;

    zzabq(zzabl zzablVar, Context context, zzabu zzabuVar, zzaae zzaaeVar) {
        this.zzUF = zzablVar;
        this.zztF = context;
        this.zzUA = zzabuVar;
        this.zzUG = zzaaeVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzUF.zzUr.zza(this.zztF, this.zzUA, this.zzUG.zzvT);
    }
}
