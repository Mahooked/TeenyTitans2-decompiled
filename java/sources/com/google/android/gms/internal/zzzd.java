package com.google.android.gms.internal;

import android.view.ViewTreeObserver;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzzd implements ViewTreeObserver.OnGlobalLayoutListener {
    private /* synthetic */ zzyx zzSb;
    private /* synthetic */ WeakReference zzSd;

    zzzd(zzyx zzyxVar, WeakReference weakReference) {
        this.zzSb = zzyxVar;
        this.zzSd = weakReference;
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        this.zzSb.zza((WeakReference<zzaka>) this.zzSd, false);
    }
}
