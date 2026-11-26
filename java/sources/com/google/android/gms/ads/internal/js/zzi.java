package com.google.android.gms.ads.internal.js;

import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
final class zzi implements Runnable {
    private /* synthetic */ zze zzKW;
    private /* synthetic */ String zzKY;

    zzi(zze zzeVar, String str) {
        this.zzKW = zzeVar;
        this.zzKY = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzKW.zzJH.loadData(this.zzKY, "text/html", HTTP.UTF_8);
    }
}
