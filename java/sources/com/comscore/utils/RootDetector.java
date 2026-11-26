package com.comscore.utils;

import android.os.Build;
import java.io.File;

/* JADX INFO: loaded from: classes.dex */
public class RootDetector {
    private static boolean a() {
        String str = Build.TAGS;
        return str != null && str.contains("test-keys");
    }

    private static boolean b() {
        try {
            return new File("/system/app/Superuser.apk").exists();
        } catch (Exception e) {
        }
    }

    public static boolean isDeviceRooted() {
        return a() || b();
    }
}
