package com.amazon.android.c;

import android.app.ActivityManager;
import android.app.Application;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Debug;
import android.os.Environment;
import android.os.StatFs;
import com.amazon.android.framework.util.KiwiLogger;
import java.io.PrintWriter;
import java.io.Serializable;
import java.io.StringWriter;
import java.math.BigInteger;
import java.security.MessageDigest;
import java.util.Date;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.commons.io.IOUtils;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public final class b implements Serializable {
    private static final KiwiLogger a = new KiwiLogger("CrashReport");
    private static final long serialVersionUID = 1;
    private final HashMap b = new LinkedHashMap();

    public b(Application application, Throwable th) {
        try {
            this.b.put("crashTime", new Date().toString());
            PackageInfo packageInfoA = a(application);
            if (packageInfoA != null) {
                this.b.put("packageVersionName", packageInfoA.versionName);
                this.b.put("packageName", packageInfoA.packageName);
                this.b.put("packageFilePath", application.getFilesDir().getAbsolutePath());
            }
            this.b.put("deviceModel", Build.MODEL);
            this.b.put("androidVersion", Build.VERSION.RELEASE);
            this.b.put("deviceBoard", Build.BOARD);
            this.b.put("deviceBrand", Build.BRAND);
            this.b.put("deviceDisplay", Build.DISPLAY);
            this.b.put("deviceFingerPrint", Build.FINGERPRINT);
            this.b.put("deviceHost", Build.HOST);
            this.b.put("deviceId", Build.ID);
            this.b.put("deviceManufacturer", Build.MANUFACTURER);
            this.b.put("deviceProduct", Build.PRODUCT);
            this.b.put("deviceTags", Build.TAGS);
            this.b.put("deviceTime", Long.toString(Build.TIME));
            this.b.put("deviceType", Build.TYPE);
            this.b.put("deviceUser", Build.USER);
            HashMap map = this.b;
            StatFs statFs = new StatFs(Environment.getDataDirectory().getPath());
            map.put("totalInternalMemorySize", Long.toString(((long) statFs.getBlockCount()) * ((long) statFs.getBlockSize())));
            HashMap map2 = this.b;
            StatFs statFs2 = new StatFs(Environment.getDataDirectory().getPath());
            map2.put("availableInternalMemorySize", Long.toString(((long) statFs2.getAvailableBlocks()) * ((long) statFs2.getBlockSize())));
            ActivityManager activityManager = (ActivityManager) application.getSystemService("activity");
            if (activityManager != null) {
                ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();
                activityManager.getMemoryInfo(memoryInfo);
                this.b.put("memLowFlag", Boolean.toString(memoryInfo.lowMemory));
                this.b.put("memLowThreshold", Long.toString(memoryInfo.threshold));
            }
            this.b.put("nativeHeapSize", Long.toString(Debug.getNativeHeapSize()));
            this.b.put("nativeHeapFreeSize", Long.toString(Debug.getNativeHeapAllocatedSize()));
            this.b.put("threadAllocCount", Long.toString(Debug.getThreadAllocCount()));
            this.b.put("threadAllocSize", Long.toString(Debug.getThreadAllocSize()));
            a(th);
            b();
            c();
        } catch (Throwable th2) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Error collection crash report details", th2);
            }
        }
    }

    private static PackageInfo a(Application application) {
        try {
            return application.getPackageManager().getPackageInfo(application.getPackageName(), 0);
        } catch (PackageManager.NameNotFoundException e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Unable to fetch package info", e);
            }
            return null;
        }
    }

    private void a(Throwable th) {
        StringBuilder sb = new StringBuilder();
        StringWriter stringWriter = new StringWriter();
        PrintWriter printWriter = new PrintWriter(stringWriter);
        th.printStackTrace(printWriter);
        sb.append(stringWriter.toString());
        sb.append(IOUtils.LINE_SEPARATOR_UNIX);
        Throwable cause = th.getCause();
        while (cause != null) {
            cause.printStackTrace(printWriter);
            sb.append(stringWriter.toString());
            cause = cause.getCause();
            sb.append("\n\n");
        }
        printWriter.close();
        this.b.put("stackTrace", sb.toString());
    }

    private void b() {
        StringBuilder sb = new StringBuilder();
        for (Map.Entry<Thread, StackTraceElement[]> entry : Thread.getAllStackTraces().entrySet()) {
            Thread key = entry.getKey();
            StackTraceElement[] value = entry.getValue();
            sb.append("Thread : " + key.getId());
            if (!com.amazon.android.framework.util.b.a(key.getName())) {
                sb.append("/" + key.getName());
            }
            sb.append(IOUtils.LINE_SEPARATOR_UNIX);
            sb.append("isAlive : " + key.isAlive() + IOUtils.LINE_SEPARATOR_UNIX);
            sb.append("isInterrupted : " + key.isInterrupted() + IOUtils.LINE_SEPARATOR_UNIX);
            sb.append("isDaemon : " + key.isDaemon() + IOUtils.LINE_SEPARATOR_UNIX);
            for (StackTraceElement stackTraceElement : value) {
                sb.append("\tat " + stackTraceElement + IOUtils.LINE_SEPARATOR_UNIX);
            }
            sb.append("\n\n");
        }
        this.b.put("threadDump", sb.toString());
    }

    private void c() {
        try {
            StringBuilder sb = new StringBuilder();
            sb.append((String) this.b.get("packageName")).append((String) this.b.get("packageVersionName")).append((String) this.b.get("androidVersion"));
            String str = (String) this.b.get("stackTrace");
            if (str != null) {
                Matcher matcher = Pattern.compile("([a-zA-Z0-9_.]+(Exception|Error))|(at\\s.*\\(.*\\))").matcher(str);
                while (matcher.find()) {
                    sb.append(matcher.group());
                }
            }
            this.b.put("crashId", new BigInteger(MessageDigest.getInstance("SHA1").digest(sb.toString().getBytes(HTTP.UTF_8))).abs().toString(16));
        } catch (Exception e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Error capturing crash id", e);
            }
        }
    }

    public final Map a() {
        return this.b;
    }
}
