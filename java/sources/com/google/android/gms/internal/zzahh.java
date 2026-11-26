package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.DownloadManager;
import android.content.Context;
import android.net.Uri;
import android.view.View;
import android.view.Window;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(11)
public class zzahh extends zzahg {
    @Override // com.google.android.gms.internal.zzahg, com.google.android.gms.internal.zzahe
    public final boolean zza(DownloadManager.Request request) {
        request.allowScanningByMediaScanner();
        request.setNotificationVisibility(1);
        return true;
    }

    @Override // com.google.android.gms.internal.zzahe
    public boolean zza(Context context, WebSettings webSettings) {
        super.zza(context, webSettings);
        return ((Boolean) zzait.zzb(new zzahi(this, context, webSettings))).booleanValue();
    }

    @Override // com.google.android.gms.internal.zzahe
    public final boolean zza(Window window) {
        window.setFlags(16777216, 16777216);
        return true;
    }

    @Override // com.google.android.gms.internal.zzahe
    public zzakb zzb(zzaka zzakaVar, boolean z) {
        return new zzale(zzakaVar, z);
    }

    @Override // com.google.android.gms.internal.zzahe
    public final Set<String> zzh(Uri uri) {
        return uri.getQueryParameterNames();
    }

    @Override // com.google.android.gms.internal.zzahe
    public WebChromeClient zzm(zzaka zzakaVar) {
        return new zzakw(zzakaVar);
    }

    @Override // com.google.android.gms.internal.zzahe
    public final boolean zzq(View view) {
        view.setLayerType(0, null);
        return true;
    }

    @Override // com.google.android.gms.internal.zzahe
    public final boolean zzr(View view) {
        view.setLayerType(1, null);
        return true;
    }
}
