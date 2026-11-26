package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzack {
    protected static void zze(zzabu zzabuVar) {
        if (zzabuVar.zzUN != null) {
            zzabuVar.zzUN.release();
        }
    }

    public abstract void zza(Context context, zzabu zzabuVar, zzaje zzajeVar);
}
