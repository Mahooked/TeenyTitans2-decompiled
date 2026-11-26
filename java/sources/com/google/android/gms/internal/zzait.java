package com.google.android.gms.internal;

import android.os.StrictMode;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzait {
    public static <T> T zzb(Callable<T> callable) {
        T tCall;
        StrictMode.ThreadPolicy threadPolicy = StrictMode.getThreadPolicy();
        try {
            try {
                StrictMode.setThreadPolicy(new StrictMode.ThreadPolicy.Builder(threadPolicy).permitDiskReads().permitDiskWrites().build());
                tCall = callable.call();
                StrictMode.setThreadPolicy(threadPolicy);
            } catch (Throwable th) {
                zzafr.zzb("Unexpected exception.", th);
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "StrictModeUtil.runWithLaxStrictMode");
                StrictMode.setThreadPolicy(threadPolicy);
                tCall = null;
            }
            return tCall;
        } catch (Throwable th2) {
            StrictMode.setThreadPolicy(threadPolicy);
            throw th2;
        }
    }

    public static <T> T zzc(Callable<T> callable) throws Exception {
        StrictMode.ThreadPolicy threadPolicy = StrictMode.getThreadPolicy();
        try {
            StrictMode.setThreadPolicy(new StrictMode.ThreadPolicy.Builder(threadPolicy).permitDiskReads().permitDiskWrites().build());
            return callable.call();
        } finally {
            StrictMode.setThreadPolicy(threadPolicy);
        }
    }
}
