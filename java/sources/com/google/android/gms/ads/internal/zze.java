package com.google.android.gms.ads.internal;

import android.webkit.CookieManager;
import com.google.android.gms.internal.zzmo;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zze implements Callable<String> {
    private /* synthetic */ zzd zzsZ;

    zze(zzd zzdVar) {
        this.zzsZ = zzdVar;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ String call() throws Exception {
        CookieManager cookieManagerZzS;
        return (!((Boolean) zzbs.zzbL().zzd(zzmo.zzGf)).booleanValue() || (cookieManagerZzS = zzbs.zzbB().zzS(this.zzsZ.zzsP.zzqD)) == null) ? "" : cookieManagerZzS.getCookie("googleads.g.doubleclick.net");
    }
}
