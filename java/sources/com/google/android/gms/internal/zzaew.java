package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import java.lang.reflect.Method;
import java.util.concurrent.Callable;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.Future;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaew {
    private final AtomicReference<ThreadPoolExecutor> zzXz = new AtomicReference<>(null);
    private final Object zzXA = new Object();

    @Nullable
    private String zzXB = null;
    private AtomicBoolean zzXC = new AtomicBoolean(false);
    private final AtomicInteger zzXD = new AtomicInteger(-1);
    private final AtomicReference<Object> zzXE = new AtomicReference<>(null);
    private final AtomicReference<Object> zzXF = new AtomicReference<>(null);
    private ConcurrentMap<String, Method> zzXG = new ConcurrentHashMap(9);

    private final Object zza(String str, Context context) {
        if (!zza(context, "com.google.android.gms.measurement.AppMeasurement", this.zzXE, true)) {
            return null;
        }
        try {
            return zzl(context, str).invoke(this.zzXE.get(), new Object[0]);
        } catch (Exception e) {
            zza(e, str, true);
            return null;
        }
    }

    private final void zza(Context context, String str, Bundle bundle) {
        if (zza(context, "com.google.android.gms.measurement.AppMeasurement", this.zzXE, true)) {
            try {
                zzx(context).invoke(this.zzXE.get(), "am", str, bundle);
            } catch (Exception e) {
                zza(e, "logEventInternal", true);
            }
        }
    }

    private final void zza(Context context, String str, String str2) {
        if (zza(context, "com.google.android.gms.measurement.AppMeasurement", this.zzXE, true)) {
            try {
                zzk(context, str2).invoke(this.zzXE.get(), str);
                String strValueOf = String.valueOf(str2);
                zzafr.v(strValueOf.length() != 0 ? "Invoke Firebase method ".concat(strValueOf) : new String("Invoke Firebase method "));
            } catch (Exception e) {
                zza(e, str2, false);
            }
        }
    }

    private final void zza(Exception exc, String str, boolean z) {
        if (this.zzXC.get()) {
            return;
        }
        zzafr.zzc(new StringBuilder(String.valueOf(str).length() + 30).append("Invoke Firebase method ").append(str).append(" error.").toString(), exc);
        if (z) {
            zzafr.zzaT("The Google Mobile Ads SDK will not integrate with Firebase. Admob/Firbase integration requires the latest Firebase SDK jar, but Firebase SDK is either missing or out of date");
            this.zzXC.set(true);
        }
    }

    private final boolean zza(Context context, String str, AtomicReference<Object> atomicReference, boolean z) {
        if (atomicReference.get() == null) {
            try {
                atomicReference.compareAndSet(null, context.getClassLoader().loadClass(str).getDeclaredMethod("getInstance", Context.class).invoke(null, context));
            } catch (Exception e) {
                zza(e, "getInstance", z);
                return false;
            }
        }
        return true;
    }

    private static Bundle zzj(Context context, String str) {
        Bundle bundle = new Bundle();
        bundle.putString("_aeid", str);
        return bundle;
    }

    private final Method zzk(Context context, String str) {
        Method method = this.zzXG.get(str);
        if (method != null) {
            return method;
        }
        try {
            Method declaredMethod = context.getClassLoader().loadClass("com.google.android.gms.measurement.AppMeasurement").getDeclaredMethod(str, String.class);
            this.zzXG.put(str, declaredMethod);
            return declaredMethod;
        } catch (Exception e) {
            zza(e, str, false);
            return null;
        }
    }

    private final Method zzl(Context context, String str) {
        Method method = this.zzXG.get(str);
        if (method != null) {
            return method;
        }
        try {
            Method declaredMethod = context.getClassLoader().loadClass("com.google.android.gms.measurement.AppMeasurement").getDeclaredMethod(str, new Class[0]);
            this.zzXG.put(str, declaredMethod);
            return declaredMethod;
        } catch (Exception e) {
            zza(e, str, false);
            return null;
        }
    }

    private final Method zzm(Context context, String str) {
        Method method = this.zzXG.get(str);
        if (method != null) {
            return method;
        }
        try {
            Method declaredMethod = context.getClassLoader().loadClass("com.google.firebase.analytics.FirebaseAnalytics").getDeclaredMethod(str, Activity.class, String.class, String.class);
            this.zzXG.put(str, declaredMethod);
            return declaredMethod;
        } catch (Exception e) {
            zza(e, str, false);
            return null;
        }
    }

    private final Method zzx(Context context) {
        Method method = this.zzXG.get("logEventInternal");
        if (method != null) {
            return method;
        }
        try {
            Method declaredMethod = context.getClassLoader().loadClass("com.google.android.gms.measurement.AppMeasurement").getDeclaredMethod("logEventInternal", String.class, String.class, Bundle.class);
            this.zzXG.put("logEventInternal", declaredMethod);
            return declaredMethod;
        } catch (Exception e) {
            zza(e, "logEventInternal", true);
            return null;
        }
    }

    public final void zzc(Context context, String str) {
        if (zzp(context)) {
            zza(context, str, "beginAdUnitExposure");
        }
    }

    public final void zzd(Context context, String str) {
        if (zzp(context)) {
            zza(context, str, "endAdUnitExposure");
        }
    }

    public final void zze(Context context, String str) {
        if (zzp(context) && (context instanceof Activity) && zza(context, "com.google.firebase.analytics.FirebaseAnalytics", this.zzXF, false)) {
            try {
                zzm(context, "setCurrentScreen").invoke(this.zzXF.get(), (Activity) context, str, context.getPackageName());
            } catch (Exception e) {
                zza(e, "setCurrentScreen", false);
            }
        }
    }

    public final void zzf(Context context, String str) {
        if (zzp(context)) {
            Bundle bundleZzj = zzj(context, str);
            bundleZzj.putInt("_r", 1);
            zza(context, "_ac", bundleZzj);
        }
    }

    public final void zzg(Context context, String str) {
        if (zzp(context)) {
            zza(context, "_ai", zzj(context, str));
        }
    }

    public final void zzh(Context context, String str) {
        if (zzp(context)) {
            zza(context, "_aq", zzj(context, str));
        }
    }

    public final void zzi(Context context, String str) {
        if (zzp(context)) {
            zza(context, "_aa", zzj(context, str));
        }
    }

    /* JADX WARN: Code duplicated, block: B:18:0x004d  */
    public final boolean zzp(Context context) {
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDs)).booleanValue() || this.zzXC.get()) {
            return false;
        }
        if (this.zzXD.get() == -1) {
            zzji.zzds();
            if (zzaiy.zzX(context)) {
                this.zzXD.set(1);
            } else {
                zzji.zzds();
                if (zzaiy.zzY(context)) {
                    zzafr.zzaT("Google Play Service is out of date, the Google Mobile Ads SDK will not integrate with Firebase. Admob/Firebase integration requires updated Google Play Service.");
                    this.zzXD.set(0);
                } else {
                    this.zzXD.set(1);
                }
            }
        }
        return this.zzXD.get() == 1;
    }

    public final boolean zzq(Context context) {
        return ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDt)).booleanValue() && zzp(context);
    }

    public final boolean zzr(Context context) {
        return ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDu)).booleanValue() && zzp(context);
    }

    public final boolean zzs(Context context) {
        return ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDv)).booleanValue() && zzp(context);
    }

    public final String zzt(Context context) {
        if (!zzp(context) || !zza(context, "com.google.android.gms.measurement.AppMeasurement", this.zzXE, true)) {
            return "";
        }
        try {
            String str = (String) zzl(context, "getCurrentScreenName").invoke(this.zzXE.get(), new Object[0]);
            if (str == null) {
                str = (String) zzl(context, "getCurrentScreenClass").invoke(this.zzXE.get(), new Object[0]);
            }
            return str == null ? "" : str;
        } catch (Exception e) {
            zza(e, "getCurrentScreenName", false);
            return "";
        }
    }

    @Nullable
    public final String zzu(Context context) {
        String str;
        if (!zzp(context)) {
            return null;
        }
        synchronized (this.zzXA) {
            if (this.zzXB != null) {
                str = this.zzXB;
            } else {
                this.zzXB = (String) zza("getGmpAppId", context);
                str = this.zzXB;
            }
        }
        return str;
    }

    @Nullable
    public final String zzv(final Context context) {
        if (!zzp(context)) {
            return null;
        }
        long jLongValue = ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDB)).longValue();
        if (jLongValue < 0) {
            return (String) zza("getAppInstanceId", context);
        }
        if (this.zzXz.get() == null) {
            this.zzXz.compareAndSet(null, new ThreadPoolExecutor(((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDC)).intValue(), ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDC)).intValue(), 1L, TimeUnit.MINUTES, new LinkedBlockingQueue(), new zzaey(this)));
        }
        Future futureSubmit = this.zzXz.get().submit(new Callable(this, context) { // from class: com.google.android.gms.internal.zzaex
            private final zzaew zzXH;
            private final Context zzXI;

            {
                this.zzXH = this;
                this.zzXI = context;
            }

            @Override // java.util.concurrent.Callable
            public final Object call() {
                return this.zzXH.zzy(this.zzXI);
            }
        });
        try {
            return (String) futureSubmit.get(jLongValue, TimeUnit.MILLISECONDS);
        } catch (Exception e) {
            futureSubmit.cancel(true);
            if (e instanceof TimeoutException) {
                return "TIME_OUT";
            }
            return null;
        }
    }

    @Nullable
    public final String zzw(Context context) {
        Object objZza;
        if (zzp(context) && (objZza = zza("generateEventId", context)) != null) {
            return objZza.toString();
        }
        return null;
    }

    final /* synthetic */ String zzy(Context context) throws Exception {
        return (String) zza("getAppInstanceId", context);
    }
}
