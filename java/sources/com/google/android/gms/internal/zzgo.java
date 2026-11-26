package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Application;

/* JADX INFO: loaded from: classes.dex */
final class zzgo implements zzgr {
    private /* synthetic */ Activity val$activity;

    zzgo(zzgj zzgjVar, Activity activity) {
        this.val$activity = activity;
    }

    @Override // com.google.android.gms.internal.zzgr
    public final void zza(Application.ActivityLifecycleCallbacks activityLifecycleCallbacks) {
        activityLifecycleCallbacks.onActivityStopped(this.val$activity);
    }
}
