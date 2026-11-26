package com.comscore.android.id;

import android.annotation.SuppressLint;
import android.os.Build;

/* JADX INFO: loaded from: classes.dex */
@SuppressLint({"NewApi"})
public class API9 {
    public static String getAndroidSerial() {
        if (Integer.valueOf(Build.VERSION.SDK_INT).intValue() >= 9) {
            return Build.SERIAL;
        }
        return null;
    }
}
