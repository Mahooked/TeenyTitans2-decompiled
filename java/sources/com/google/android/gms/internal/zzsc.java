package com.google.android.gms.internal;

import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzsc implements Runnable {
    private /* synthetic */ String zzJR;
    private /* synthetic */ int zzJS;
    private /* synthetic */ int zzJT;
    private /* synthetic */ boolean zzJU = false;
    private /* synthetic */ zzsb zzJV;
    private /* synthetic */ String zzsD;

    zzsc(zzsb zzsbVar, String str, String str2, int i, int i2, boolean z) {
        this.zzJV = zzsbVar;
        this.zzsD = str;
        this.zzJR = str2;
        this.zzJS = i;
        this.zzJT = i2;
    }

    @Override // java.lang.Runnable
    public final void run() {
        HashMap map = new HashMap();
        map.put("event", "precacheProgress");
        map.put("src", this.zzsD);
        map.put("cachedSrc", this.zzJR);
        map.put("bytesLoaded", Integer.toString(this.zzJS));
        map.put("totalBytes", Integer.toString(this.zzJT));
        map.put("cacheReady", this.zzJU ? "1" : "0");
        this.zzJV.zza("onPrecacheEvent", (Map<String, String>) map);
    }
}
