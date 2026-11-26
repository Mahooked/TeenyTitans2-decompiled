package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzse implements Runnable {
    private /* synthetic */ String val$message;
    private /* synthetic */ String zzJR;
    private /* synthetic */ zzsb zzJV;
    private /* synthetic */ String zzJW;
    private /* synthetic */ String zzsD;

    zzse(zzsb zzsbVar, String str, String str2, String str3, String str4) {
        this.zzJV = zzsbVar;
        this.zzsD = str;
        this.zzJR = str2;
        this.zzJW = str3;
        this.val$message = str4;
    }

    @Override // java.lang.Runnable
    public final void run() {
        HashMap map = new HashMap();
        map.put("event", "precacheCanceled");
        map.put("src", this.zzsD);
        if (!TextUtils.isEmpty(this.zzJR)) {
            map.put("cachedSrc", this.zzJR);
        }
        zzsb zzsbVar = this.zzJV;
        map.put("type", zzsb.zzV(this.zzJW));
        map.put("reason", this.zzJW);
        if (!TextUtils.isEmpty(this.val$message)) {
            map.put("message", this.val$message);
        }
        this.zzJV.zza("onPrecacheEvent", (Map<String, String>) map);
    }
}
