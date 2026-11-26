package com.google.android.gms.internal;

import android.view.ViewTreeObserver;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
final class zzze implements ViewTreeObserver.OnScrollChangedListener {
    private /* synthetic */ zzyx zzSb;
    private /* synthetic */ WeakReference zzSd;

    zzze(zzyx zzyxVar, WeakReference weakReference) {
        this.zzSb = zzyxVar;
        this.zzSd = weakReference;
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        this.zzSb.zza((WeakReference<zzaka>) this.zzSd, true);
    }
}
