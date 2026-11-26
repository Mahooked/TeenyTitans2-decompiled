package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.PackageManager;
import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgy {
    private static Context zzaKh;
    private static Boolean zzaKi;

    public static synchronized boolean zzaN(Context context) {
        boolean zBooleanValue;
        Context applicationContext = context.getApplicationContext();
        if (zzaKh == null || zzaKi == null || zzaKh != applicationContext) {
            zzaKi = null;
            if (com.google.android.gms.common.util.zzq.isAtLeastO()) {
                try {
                    zzaKi = (Boolean) PackageManager.class.getDeclaredMethod("isInstantApp", new Class[0]).invoke(applicationContext.getPackageManager(), new Object[0]);
                } catch (IllegalAccessException | NoSuchMethodException | InvocationTargetException e) {
                    zzaKi = false;
                }
            } else {
                try {
                    context.getClassLoader().loadClass("com.google.android.instantapps.supervisor.InstantAppsRuntime");
                    zzaKi = true;
                } catch (ClassNotFoundException e2) {
                    zzaKi = false;
                }
            }
            zzaKh = applicationContext;
            zBooleanValue = zzaKi.booleanValue();
        } else {
            zBooleanValue = zzaKi.booleanValue();
        }
        return zBooleanValue;
    }
}
