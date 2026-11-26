package com.google.android.gms.internal;

import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzsd implements Runnable {
    private /* synthetic */ String zzJR;
    private /* synthetic */ int zzJT;
    private /* synthetic */ zzsb zzJV;
    private /* synthetic */ String zzsD;

    zzsd(zzsb zzsbVar, String str, String str2, int i) {
        this.zzJV = zzsbVar;
        this.zzsD = str;
        this.zzJR = str2;
        this.zzJT = i;
    }

    @Override // java.lang.Runnable
    public final void run() {
        HashMap map = new HashMap();
        map.put("event", "precacheComplete");
        map.put("src", this.zzsD);
        map.put("cachedSrc", this.zzJR);
        map.put("totalBytes", Integer.toString(this.zzJT));
        this.zzJV.zza("onPrecacheEvent", (Map<String, String>) map);
    }
}
