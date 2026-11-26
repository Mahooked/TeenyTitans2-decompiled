package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.net.http.SslError;
import android.webkit.WebChromeClient;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(14)
public class zzahj extends zzahh {
    @Override // com.google.android.gms.internal.zzahe
    public final String zza(SslError sslError) {
        return sslError.getUrl();
    }

    @Override // com.google.android.gms.internal.zzahe
    public int zzhX() {
        return 1;
    }

    @Override // com.google.android.gms.internal.zzahh, com.google.android.gms.internal.zzahe
    public final WebChromeClient zzm(zzaka zzakaVar) {
        return new zzalf(zzakaVar);
    }
}
