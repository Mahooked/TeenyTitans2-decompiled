package com.google.android.gms.internal;

import android.R;
import android.annotation.TargetApi;
import android.content.Context;
import android.webkit.CookieManager;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(21)
public final class zzaho extends zzahn {
    @Override // com.google.android.gms.internal.zzahe
    public final CookieManager zzS(Context context) {
        try {
            return CookieManager.getInstance();
        } catch (Exception e) {
            zzafr.zzb("Failed to obtain CookieManager.", e);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "ApiLevelUtil.getCookieManager");
            return null;
        }
    }

    @Override // com.google.android.gms.internal.zzahh, com.google.android.gms.internal.zzahe
    public final zzakb zzb(zzaka zzakaVar, boolean z) {
        return new zzalh(zzakaVar, z);
    }

    @Override // com.google.android.gms.internal.zzahj, com.google.android.gms.internal.zzahe
    public final int zzhX() {
        return R.style.Theme.Material.Dialog.Alert;
    }
}
