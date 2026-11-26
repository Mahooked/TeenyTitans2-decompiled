package com.google.android.gms.internal;

import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzafp implements zzahp<Future> {
    private volatile Thread zzYV;
    private final Runnable zzv = new zzafq(this);
    private boolean zzYW = false;

    public zzafp() {
    }

    public zzafp(boolean z) {
    }

    @Override // com.google.android.gms.internal.zzahp
    public final void cancel() {
        onStop();
        if (this.zzYV != null) {
            this.zzYV.interrupt();
        }
    }

    public abstract void onStop();

    public abstract void zzbd();

    @Override // com.google.android.gms.internal.zzahp
    public final /* synthetic */ Future zzgp() {
        return this.zzYW ? zzagt.zza(1, this.zzv) : zzagt.zza(this.zzv);
    }

    public final Future zzhL() {
        return this.zzYW ? zzagt.zza(1, this.zzv) : zzagt.zza(this.zzv);
    }
}
