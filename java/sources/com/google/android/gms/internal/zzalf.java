package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.view.View;
import android.webkit.WebChromeClient;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzalf extends zzakw {
    public zzalf(zzaka zzakaVar) {
        super(zzakaVar);
    }

    @Override // android.webkit.WebChromeClient
    public final void onShowCustomView(View view, int i, WebChromeClient.CustomViewCallback customViewCallback) {
        zza(view, i, customViewCallback);
    }
}
