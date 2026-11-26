package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(11)
public final class zzale extends zzalg {
    public zzale(zzaka zzakaVar, boolean z) {
        super(zzakaVar, z);
    }

    @Override // com.google.android.gms.internal.zzakb, android.webkit.WebViewClient
    public final WebResourceResponse shouldInterceptRequest(WebView webView, String str) {
        return zza(webView, str, (Map<String, String>) null);
    }
}
