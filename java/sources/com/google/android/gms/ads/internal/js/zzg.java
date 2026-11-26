package com.google.android.gms.ads.internal.js;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements Runnable {
    private /* synthetic */ String zzKU;
    private /* synthetic */ zze zzKW;
    private /* synthetic */ String zzKX;

    zzg(zze zzeVar, String str, String str2) {
        this.zzKW = zzeVar;
        this.zzKU = str;
        this.zzKX = str2;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzKW.zzJH.zzi(this.zzKU, this.zzKX);
    }
}
