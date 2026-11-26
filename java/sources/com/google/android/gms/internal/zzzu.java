package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzzu {
    /* JADX INFO: Access modifiers changed from: private */
    public static boolean zzc(Context context, boolean z) {
        if (!z) {
            return true;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        int iZzP = zzagz.zzP(context);
        if (iZzP == 0) {
            return false;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        return iZzP <= zzagz.zzQ(context);
    }
}
