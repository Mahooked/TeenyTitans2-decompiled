package com.google.android.gms.ads.internal.js;

/* JADX INFO: loaded from: classes.dex */
final class zzj implements Runnable {
    private /* synthetic */ String zzKS;
    private /* synthetic */ zze zzKW;

    zzj(zze zzeVar, String str) {
        this.zzKW = zzeVar;
        this.zzKS = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzKW.zzJH.loadUrl(this.zzKS);
    }
}
