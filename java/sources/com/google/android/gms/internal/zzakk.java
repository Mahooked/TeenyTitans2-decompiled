package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.internal.zzbl;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzakk {
    public final zzaka zza(Context context, zziv zzivVar, boolean z, boolean z2, @Nullable zzcu zzcuVar, zzaje zzajeVar, zznb zznbVar, zzbl zzblVar, com.google.android.gms.ads.internal.zzv zzvVar, zzig zzigVar) throws zzakm {
        try {
            return (zzaka) zzait.zzc(new zzakl(this, context, zzivVar, z, z2, zzcuVar, zzajeVar, zznbVar, zzblVar, zzvVar, zzigVar));
        } catch (Throwable th) {
            throw new zzakm(this, "Webview initialization failed.", th);
        }
    }
}
