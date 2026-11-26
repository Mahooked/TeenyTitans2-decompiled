package com.google.android.gms.internal;

import android.util.Log;
import com.google.ads.AdRequest;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafr extends zzajc {
    public static void v(String str) {
        if (zzhM()) {
            Log.v(AdRequest.LOGTAG, str);
        }
    }

    public static boolean zzhM() {
        if (zzz(2)) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEp)).booleanValue()) {
                return true;
            }
        }
        return false;
    }
}
