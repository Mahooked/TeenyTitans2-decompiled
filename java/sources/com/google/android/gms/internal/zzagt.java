package com.google.android.gms.internal;

import java.util.concurrent.Callable;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.RejectedExecutionException;
import java.util.concurrent.ThreadFactory;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzagt {
    private static final ThreadPoolExecutor zzZk = new ThreadPoolExecutor(20, 20, 1, TimeUnit.MINUTES, new LinkedBlockingQueue(), zzaH("Default"));
    private static final ThreadPoolExecutor zzZl = new ThreadPoolExecutor(5, 5, 1, TimeUnit.MINUTES, new LinkedBlockingQueue(), zzaH("Loader"));

    static {
        zzZk.allowCoreThreadTimeOut(true);
        zzZl.allowCoreThreadTimeOut(true);
    }

    public static zzajm<Void> zza(int i, Runnable runnable) {
        return i == 1 ? zza(zzZl, new zzagu(runnable)) : zza(zzZk, new zzagv(runnable));
    }

    public static zzajm<Void> zza(Runnable runnable) {
        return zza(0, runnable);
    }

    public static <T> zzajm<T> zza(Callable<T> callable) {
        return zza(zzZk, callable);
    }

    public static <T> zzajm<T> zza(ExecutorService executorService, Callable<T> callable) {
        zzajg zzajgVar = new zzajg();
        try {
            zzajgVar.zzd(new zzagx(zzajgVar, executorService.submit(new zzagw(zzajgVar, callable))));
        } catch (RejectedExecutionException e) {
            zzafr.zzc("Thread execution is rejected.", e);
            zzajgVar.cancel(true);
        }
        return zzajgVar;
    }

    private static ThreadFactory zzaH(String str) {
        return new zzagy(str);
    }
}
