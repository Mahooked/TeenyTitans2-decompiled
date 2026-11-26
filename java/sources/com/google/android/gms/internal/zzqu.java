package com.google.android.gms.internal;

import android.util.DisplayMetrics;
import android.view.View;
import android.view.WindowManager;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzqu implements zzrd {
    zzqu() {
    }

    /* JADX WARN: Multi-variable type inference failed */
    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        WindowManager windowManager = (WindowManager) zzakaVar.getContext().getSystemService("window");
        com.google.android.gms.ads.internal.zzbs.zzbz();
        DisplayMetrics displayMetricsZza = zzagz.zza(windowManager);
        int i = displayMetricsZza.widthPixels;
        int i2 = displayMetricsZza.heightPixels;
        int[] iArr = new int[2];
        HashMap map2 = new HashMap();
        ((View) zzakaVar).getLocationInWindow(iArr);
        map2.put("xInPixels", Integer.valueOf(iArr[0]));
        map2.put("yInPixels", Integer.valueOf(iArr[1]));
        map2.put("windowWidthInPixels", Integer.valueOf(i));
        map2.put("windowHeightInPixels", Integer.valueOf(i2));
        zzakaVar.zza("locationReady", map2);
        zzafr.zzaT("GET LOCATION COMPILED");
    }
}
