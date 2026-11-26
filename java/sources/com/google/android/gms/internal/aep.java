package com.google.android.gms.internal;

import android.content.ComponentName;
import android.support.customtabs.CustomTabsClient;
import android.support.customtabs.CustomTabsServiceConnection;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
public final class aep extends CustomTabsServiceConnection {
    private WeakReference<aeq> zzcuA;

    public aep(aeq aeqVar) {
        this.zzcuA = new WeakReference<>(aeqVar);
    }

    public final void onCustomTabsServiceConnected(ComponentName componentName, CustomTabsClient customTabsClient) {
        aeq aeqVar = this.zzcuA.get();
        if (aeqVar != null) {
            aeqVar.zza(customTabsClient);
        }
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        aeq aeqVar = this.zzcuA.get();
        if (aeqVar != null) {
            aeqVar.zzdZ();
        }
    }
}
