package com.google.android.gms.common.util;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.support.v4.view.MotionEventCompat;

/* JADX INFO: loaded from: classes.dex */
public final class zzj {
    private static Boolean zzaJJ;
    private static Boolean zzaJK;
    private static Boolean zzaJL;
    private static Boolean zzaJM;
    private static Boolean zzaJN;

    /* JADX WARN: Code duplicated, block: B:21:0x003a  */
    public static boolean zza(Resources resources) {
        boolean z;
        if (resources == null) {
            return false;
        }
        if (zzaJJ == null) {
            if (!((resources.getConfiguration().screenLayout & 15) > 3)) {
                if (zzaJK == null) {
                    Configuration configuration = resources.getConfiguration();
                    zzaJK = Boolean.valueOf((configuration.screenLayout & 15) <= 3 && configuration.smallestScreenWidthDp >= 600);
                }
                z = zzaJK.booleanValue();
            }
            zzaJJ = Boolean.valueOf(z);
        }
        return zzaJJ.booleanValue();
    }

    @TargetApi(20)
    public static boolean zzaG(Context context) {
        if (zzaJL == null) {
            zzaJL = Boolean.valueOf(zzq.zzsd() && context.getPackageManager().hasSystemFeature("android.hardware.type.watch"));
        }
        return zzaJL.booleanValue();
    }

    @TargetApi(MotionEventCompat.AXIS_DISTANCE)
    public static boolean zzaH(Context context) {
        return (!zzq.isAtLeastN() || zzaI(context)) && zzaG(context);
    }

    @TargetApi(21)
    public static boolean zzaI(Context context) {
        if (zzaJM == null) {
            zzaJM = Boolean.valueOf(zzq.zzse() && context.getPackageManager().hasSystemFeature("cn.google"));
        }
        return zzaJM.booleanValue();
    }

    public static boolean zzaJ(Context context) {
        if (zzaJN == null) {
            zzaJN = Boolean.valueOf(context.getPackageManager().hasSystemFeature("android.hardware.type.iot") || context.getPackageManager().hasSystemFeature("android.hardware.type.embedded"));
        }
        return zzaJN.booleanValue();
    }
}
