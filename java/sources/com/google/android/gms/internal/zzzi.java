package com.google.android.gms.internal;

import android.content.Context;
import android.net.Uri;
import android.os.Build;
import android.os.Looper;
import android.support.v4.os.EnvironmentCompat;
import android.text.TextUtils;
import com.adobe.mobile.TargetLocationRequest;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzzi implements zzzl {
    private final String mPackageName;
    private final Object zzSh = new Object();
    private final WeakHashMap<Thread, Boolean> zzSi = new WeakHashMap<>();
    private final zzaje zzuK;
    private static final Object zzuF = new Object();
    private static zzzl zzSg = null;

    private zzzi(String str, zzaje zzajeVar) {
        this.mPackageName = str;
        this.zzuK = zzajeVar;
        Thread thread = Looper.getMainLooper().getThread();
        if (thread != null) {
            synchronized (this.zzSh) {
                this.zzSi.put(thread, true);
            }
            thread.setUncaughtExceptionHandler(new zzzk(this, thread.getUncaughtExceptionHandler()));
        }
        Thread.setDefaultUncaughtExceptionHandler(new zzzj(this, Thread.getDefaultUncaughtExceptionHandler()));
    }

    private static boolean zzat(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        if (str.startsWith((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCe))) {
            return true;
        }
        try {
            return Class.forName(str).isAnnotationPresent(zzzn.class);
        } catch (Exception e) {
            String strValueOf = String.valueOf(str);
            zzafr.zza(strValueOf.length() != 0 ? "Fail to check class type for class ".concat(strValueOf) : new String("Fail to check class type for class "), e);
            return false;
        }
    }

    public static zzzl zzc(Context context, zzaje zzajeVar) {
        synchronized (zzuF) {
            if (zzSg == null) {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCc)).booleanValue()) {
                    String packageName = EnvironmentCompat.MEDIA_UNKNOWN;
                    try {
                        packageName = context.getApplicationContext().getPackageName();
                    } catch (Throwable th) {
                        zzafr.zzaT("Cannot obtain package name, proceeding.");
                    }
                    zzSg = new zzzi(packageName, zzajeVar);
                } else {
                    zzSg = new zzzm();
                }
            }
        }
        return zzSg;
    }

    /* JADX WARN: Code duplicated, block: B:21:0x0046  */
    protected final void zza(Thread thread, Throwable th) {
        boolean z;
        if (th != null) {
            boolean z2 = false;
            boolean z3 = false;
            for (Throwable cause = th; cause != null; cause = cause.getCause()) {
                for (StackTraceElement stackTraceElement : cause.getStackTrace()) {
                    if (zzat(stackTraceElement.getClassName())) {
                        z3 = true;
                    }
                    if (getClass().getName().equals(stackTraceElement.getClassName())) {
                        z2 = true;
                    }
                }
            }
            z = z3 && !z2;
        }
        if (z) {
            zza(th, "");
        }
    }

    @Override // com.google.android.gms.internal.zzzl
    public final void zza(Throwable th, String str) {
        Throwable th2;
        Throwable th3;
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCd)).booleanValue()) {
            th2 = th;
        } else {
            LinkedList linkedList = new LinkedList();
            for (Throwable cause = th; cause != null; cause = cause.getCause()) {
                linkedList.push(cause);
            }
            th2 = null;
            while (!linkedList.isEmpty()) {
                Throwable th4 = (Throwable) linkedList.pop();
                StackTraceElement[] stackTrace = th4.getStackTrace();
                ArrayList arrayList = new ArrayList();
                arrayList.add(new StackTraceElement(th4.getClass().getName(), "<filtered>", "<filtered>", 1));
                boolean z = false;
                for (StackTraceElement stackTraceElement : stackTrace) {
                    if (zzat(stackTraceElement.getClassName())) {
                        z = true;
                        arrayList.add(stackTraceElement);
                    } else {
                        String className = stackTraceElement.getClassName();
                        if (!TextUtils.isEmpty(className) && (className.startsWith("android.") || className.startsWith("java."))) {
                            arrayList.add(stackTraceElement);
                        } else {
                            arrayList.add(new StackTraceElement("<filtered>", "<filtered>", "<filtered>", 1));
                        }
                    }
                }
                if (z) {
                    th3 = th2 == null ? new Throwable(th4.getMessage()) : new Throwable(th4.getMessage(), th2);
                    th3.setStackTrace((StackTraceElement[]) arrayList.toArray(new StackTraceElement[0]));
                } else {
                    th3 = th2;
                }
                th2 = th3;
            }
        }
        if (th2 == null) {
            return;
        }
        Class<?> cls = th.getClass();
        ArrayList arrayList2 = new ArrayList();
        StringWriter stringWriter = new StringWriter();
        th2.printStackTrace(new PrintWriter(stringWriter));
        Uri.Builder builderAppendQueryParameter = new Uri.Builder().scheme("https").path("//pagead2.googlesyndication.com/pagead/gen_204").appendQueryParameter(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, "gmob-apps-report-exception").appendQueryParameter("os", Build.VERSION.RELEASE).appendQueryParameter("api", String.valueOf(Build.VERSION.SDK_INT));
        com.google.android.gms.ads.internal.zzbs.zzbz();
        arrayList2.add(builderAppendQueryParameter.appendQueryParameter("device", zzagz.zzhQ()).appendQueryParameter("js", this.zzuK.zzaP).appendQueryParameter("appid", this.mPackageName).appendQueryParameter("exceptiontype", cls.getName()).appendQueryParameter("stacktrace", stringWriter.toString()).appendQueryParameter("eids", TextUtils.join(",", zzmo.zzdJ())).appendQueryParameter("exceptionkey", str).appendQueryParameter("cl", "162978962").appendQueryParameter("rc", "dev").appendQueryParameter("session_id", com.google.android.gms.ads.internal.zzbs.zzbD().getSessionId()).toString());
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zza(arrayList2, com.google.android.gms.ads.internal.zzbs.zzbD().zzht());
    }
}
