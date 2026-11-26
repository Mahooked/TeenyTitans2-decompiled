package com.google.android.gms.internal;

import android.view.View;
import android.view.ViewTreeObserver;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzajv {
    public static void zza(View view, ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener) {
        new zzajw(view, onGlobalLayoutListener).zzio();
    }

    public static void zza(View view, ViewTreeObserver.OnScrollChangedListener onScrollChangedListener) {
        new zzajx(view, onScrollChangedListener).zzio();
    }
}
