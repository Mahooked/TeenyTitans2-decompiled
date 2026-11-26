package com.google.android.gms.internal;

import android.content.Context;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzxz extends zzxr implements zzakf {
    zzxz(Context context, zzafg zzafgVar, zzaka zzakaVar, zzxy zzxyVar) {
        super(context, zzafgVar, zzakaVar, zzxyVar);
    }

    @Override // com.google.android.gms.internal.zzxr
    protected final void zzgo() {
        if (this.zzQR.errorCode != -2) {
            return;
        }
        this.zzJH.zziw().zza(this);
        zzgq();
        zzafr.zzaC("Loading HTML in WebView.");
        this.zzJH.loadDataWithBaseURL(this.zzQR.zzPi, this.zzQR.body, "text/html", HTTP.UTF_8, null);
    }

    protected void zzgq() {
    }
}
