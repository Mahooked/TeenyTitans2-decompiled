package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Application;
import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
final class zzgk implements zzgr {
    private /* synthetic */ Activity val$activity;
    private /* synthetic */ Bundle zzxV;

    zzgk(zzgj zzgjVar, Activity activity, Bundle bundle) {
        this.val$activity = activity;
        this.zzxV = bundle;
    }

    @Override // com.google.android.gms.internal.zzgr
    public final void zza(Application.ActivityLifecycleCallbacks activityLifecycleCallbacks) {
        activityLifecycleCallbacks.onActivityCreated(this.val$activity, this.zzxV);
    }
}
