package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Application;

/* JADX INFO: loaded from: classes.dex */
final class zzgq implements zzgr {
    private /* synthetic */ Activity val$activity;

    zzgq(zzgj zzgjVar, Activity activity) {
        this.val$activity = activity;
    }

    @Override // com.google.android.gms.internal.zzgr
    public final void zza(Application.ActivityLifecycleCallbacks activityLifecycleCallbacks) {
        activityLifecycleCallbacks.onActivityDestroyed(this.val$activity);
    }
}
