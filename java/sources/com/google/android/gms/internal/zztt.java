package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zztt {
    private final Object mLock = new Object();
    private zztu zzLD;

    public final zztu zzb(Context context, zzaje zzajeVar) {
        zztu zztuVar;
        synchronized (this.mLock) {
            if (this.zzLD == null) {
                Context applicationContext = context.getApplicationContext();
                if (applicationContext != null) {
                    context = applicationContext;
                }
                this.zzLD = new zztu(context, zzajeVar, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzBX));
            }
            zztuVar = this.zzLD;
        }
        return zztuVar;
    }
}
