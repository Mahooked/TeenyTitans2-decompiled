package com.google.android.gms.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
public final class zzbdw<L> {
    private volatile L mListener;
    private final zzbdx zzaEM;
    private final zzbdy<L> zzaEN;

    zzbdw(@NonNull Looper looper, @NonNull L l, @NonNull String str) {
        this.zzaEM = new zzbdx(this, looper);
        this.mListener = (L) zzbo.zzb(l, "Listener must not be null");
        this.zzaEN = new zzbdy<>(l, zzbo.zzcF(str));
    }

    public final void clear() {
        this.mListener = null;
    }

    public final void zza(zzbdz<? super L> zzbdzVar) {
        zzbo.zzb(zzbdzVar, "Notifier must not be null");
        this.zzaEM.sendMessage(this.zzaEM.obtainMessage(1, zzbdzVar));
    }

    final void zzb(zzbdz<? super L> zzbdzVar) {
        L l = this.mListener;
        if (l == null) {
            zzbdzVar.zzpT();
            return;
        }
        try {
            zzbdzVar.zzq(l);
        } catch (RuntimeException e) {
            zzbdzVar.zzpT();
            throw e;
        }
    }

    public final boolean zzoc() {
        return this.mListener != null;
    }

    @NonNull
    public final zzbdy<L> zzqG() {
        return this.zzaEN;
    }
}
