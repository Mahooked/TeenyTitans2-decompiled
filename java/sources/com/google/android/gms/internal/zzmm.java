package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.ConditionVariable;
import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmm {
    private final Object mLock = new Object();
    private final ConditionVariable zzBS = new ConditionVariable();
    private volatile boolean zzuH = false;

    @Nullable
    private SharedPreferences zzBT = null;

    public final void initialize(Context context) {
        if (this.zzuH) {
            return;
        }
        synchronized (this.mLock) {
            if (this.zzuH) {
                return;
            }
            try {
                Context remoteContext = com.google.android.gms.common.zzo.getRemoteContext(context);
                if (remoteContext != null || context == null) {
                    context = remoteContext;
                } else {
                    Context applicationContext = context.getApplicationContext();
                    if (applicationContext != null) {
                        context = applicationContext;
                    }
                }
                if (context == null) {
                    this.zzBS.open();
                    return;
                }
                com.google.android.gms.ads.internal.zzbs.zzbJ();
                this.zzBT = context.getSharedPreferences("google_ads_flags", 0);
                this.zzuH = true;
                this.zzBS.open();
            } catch (Throwable th) {
                this.zzBS.open();
                throw th;
            }
        }
    }

    public final <T> T zzd(zzme<T> zzmeVar) {
        if (!this.zzBS.block(5000L)) {
            throw new IllegalStateException("Flags.initialize() was not called!");
        }
        if (!this.zzuH || this.zzBT == null) {
            synchronized (this.mLock) {
                if (!this.zzuH || this.zzBT == null) {
                    return zzmeVar.zzdI();
                }
            }
        }
        return (T) zzait.zzb(new zzmn(this, zzmeVar));
    }
}
