package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Application;
import android.os.Bundle;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzgj implements Application.ActivityLifecycleCallbacks {
    private final WeakReference<Application.ActivityLifecycleCallbacks> zzxT;
    private boolean zzxU = false;
    private final Application zzxz;

    public zzgj(Application application, Application.ActivityLifecycleCallbacks activityLifecycleCallbacks) {
        this.zzxT = new WeakReference<>(activityLifecycleCallbacks);
        this.zzxz = application;
    }

    private final void zza(zzgr zzgrVar) {
        try {
            Application.ActivityLifecycleCallbacks activityLifecycleCallbacks = this.zzxT.get();
            if (activityLifecycleCallbacks != null) {
                zzgrVar.zza(activityLifecycleCallbacks);
            } else if (!this.zzxU) {
                this.zzxz.unregisterActivityLifecycleCallbacks(this);
                this.zzxU = true;
            }
        } catch (Exception e) {
            zzafr.zzb("Error while dispatching lifecycle callback.", e);
        }
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityCreated(Activity activity, Bundle bundle) {
        zza(new zzgk(this, activity, bundle));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityDestroyed(Activity activity) {
        zza(new zzgq(this, activity));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityPaused(Activity activity) {
        zza(new zzgn(this, activity));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityResumed(Activity activity) {
        zza(new zzgm(this, activity));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
        zza(new zzgp(this, activity, bundle));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStarted(Activity activity) {
        zza(new zzgl(this, activity));
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStopped(Activity activity) {
        zza(new zzgo(this, activity));
    }
}
