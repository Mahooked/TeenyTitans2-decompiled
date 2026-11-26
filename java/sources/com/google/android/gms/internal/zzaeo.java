package com.google.android.gms.internal;

import java.util.TimerTask;

/* JADX INFO: loaded from: classes.dex */
final class zzaeo extends TimerTask {
    private /* synthetic */ zzaen zzXn;

    zzaeo(zzaen zzaenVar) {
        this.zzXn = zzaenVar;
    }

    @Override // java.util.TimerTask, java.lang.Runnable
    public final void run() {
        this.zzXn.zzXm.zza(this.zzXn.zzXm.zzXj, this.zzXn.zzXl, this.zzXn.zzXk);
    }
}
