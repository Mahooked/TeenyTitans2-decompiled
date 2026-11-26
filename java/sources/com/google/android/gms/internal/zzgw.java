package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(14)
final class zzgw implements Application.ActivityLifecycleCallbacks {

    @Nullable
    private Activity mActivity;
    private Context mContext;
    private Runnable zzyu;
    private long zzyv;
    private final Object mLock = new Object();
    private boolean zzyq = true;
    private boolean zzyr = false;
    private final List<zzgy> zzys = new ArrayList();
    private final List<zzhl> zzyt = new ArrayList();
    private boolean zzuH = false;

    zzgw() {
    }

    private final void setActivity(Activity activity) {
        synchronized (this.mLock) {
            if (!activity.getClass().getName().startsWith("com.google.android.gms.ads")) {
                this.mActivity = activity;
            }
        }
    }

    static /* synthetic */ boolean zza(zzgw zzgwVar, boolean z) {
        zzgwVar.zzyq = false;
        return false;
    }

    @Nullable
    public final Activity getActivity() {
        return this.mActivity;
    }

    @Nullable
    public final Context getContext() {
        return this.mContext;
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityCreated(Activity activity, Bundle bundle) {
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityDestroyed(Activity activity) {
        synchronized (this.mLock) {
            if (this.mActivity == null) {
                return;
            }
            if (this.mActivity.equals(activity)) {
                this.mActivity = null;
            }
            Iterator<zzhl> it = this.zzyt.iterator();
            while (it.hasNext()) {
                try {
                    if (it.next().zza(activity)) {
                        it.remove();
                    }
                } catch (Exception e) {
                    com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "AppActivityTracker.ActivityListener.onActivityDestroyed");
                    zzafr.zzb("onActivityStateChangedListener threw exception.", e);
                }
            }
        }
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityPaused(Activity activity) {
        setActivity(activity);
        synchronized (this.mLock) {
            Iterator<zzhl> it = this.zzyt.iterator();
            while (it.hasNext()) {
                it.next();
            }
        }
        this.zzyr = true;
        if (this.zzyu != null) {
            zzagz.zzZr.removeCallbacks(this.zzyu);
        }
        Handler handler = zzagz.zzZr;
        zzgx zzgxVar = new zzgx(this);
        this.zzyu = zzgxVar;
        handler.postDelayed(zzgxVar, this.zzyv);
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityResumed(Activity activity) {
        setActivity(activity);
        this.zzyr = false;
        boolean z = this.zzyq ? false : true;
        this.zzyq = true;
        if (this.zzyu != null) {
            zzagz.zzZr.removeCallbacks(this.zzyu);
        }
        synchronized (this.mLock) {
            Iterator<zzhl> it = this.zzyt.iterator();
            while (it.hasNext()) {
                it.next();
            }
            if (z) {
                Iterator<zzgy> it2 = this.zzys.iterator();
                while (it2.hasNext()) {
                    try {
                        it2.next().zzf(true);
                    } catch (Exception e) {
                        zzafr.zzb("OnForegroundStateChangedListener threw exception.", e);
                    }
                }
            } else {
                zzafr.zzaC("App is still foreground.");
            }
        }
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStarted(Activity activity) {
        setActivity(activity);
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStopped(Activity activity) {
    }

    public final void zza(Application application, Context context) {
        if (this.zzuH) {
            return;
        }
        application.registerActivityLifecycleCallbacks(this);
        if (context instanceof Activity) {
            setActivity((Activity) context);
        }
        this.mContext = context;
        this.zzyv = ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDK)).longValue();
        this.zzuH = true;
    }

    public final void zza(zzgy zzgyVar) {
        synchronized (this.mLock) {
            this.zzys.add(zzgyVar);
        }
    }
}
