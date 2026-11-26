package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzgv {
    private final Object zzyn = new Object();
    private zzgw zzyo = null;
    private boolean zzyp = false;

    @Nullable
    public final Activity getActivity() {
        Activity activity;
        synchronized (this.zzyn) {
            activity = this.zzyo != null ? this.zzyo.getActivity() : null;
        }
        return activity;
    }

    @Nullable
    public final Context getContext() {
        Context context;
        synchronized (this.zzyn) {
            context = this.zzyo != null ? this.zzyo.getContext() : null;
        }
        return context;
    }

    public final void initialize(Context context) {
        synchronized (this.zzyn) {
            if (!this.zzyp) {
                if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDJ)).booleanValue()) {
                    return;
                }
                Context applicationContext = context.getApplicationContext();
                if (applicationContext == null) {
                    applicationContext = context;
                }
                Application application = applicationContext instanceof Application ? (Application) applicationContext : null;
                if (application == null) {
                    zzafr.zzaT("Can not cast Context to Application");
                    return;
                }
                if (this.zzyo == null) {
                    this.zzyo = new zzgw();
                }
                this.zzyo.zza(application, context);
                this.zzyp = true;
            }
        }
    }

    public final void zza(zzgy zzgyVar) {
        synchronized (this.zzyn) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDJ)).booleanValue()) {
                if (this.zzyo == null) {
                    this.zzyo = new zzgw();
                }
                this.zzyo.zza(zzgyVar);
            }
        }
    }
}
