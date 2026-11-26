package com.comscore.android.id;

import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Looper;
import android.provider.Settings;
import android.support.v4.os.EnvironmentCompat;
import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class IdHelperAndroid {
    public static final String NO_ID_AVAILABLE = "none";
    public static final String[] INVALID_ID_VALUES = {"0123456789ABCDEF", "0123456789abcdef", "9774d56d682e549c", "9774D56D682E549C", EnvironmentCompat.MEDIA_UNKNOWN, "UNKNOWN", "android_id", "ANDROID_ID"};
    private static boolean c = false;
    private static final String a = "com.google.android.gms";
    private static final String b = "com.google.android.gms.ads.identifier.service.START";
    private static final boolean d = false;

    private static String a(Context context) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        String id = "";
        b bVar = new b();
        Intent intent = new Intent(b);
        intent.setPackage(a);
        if (context.bindService(intent, bVar, 1)) {
            try {
                id = new c(bVar.getBinder()).getId();
            } catch (Exception e) {
            } finally {
                context.unbindService(bVar);
            }
        }
        return id;
    }

    private static boolean b(Context context) {
        if (Build.VERSION.SDK_INT <= 4) {
            return false;
        }
        if (d) {
            return true;
        }
        return API4.isPackageInstalledFromGooglePlayStore(context);
    }

    public static boolean checkAndroidId(String str) {
        for (int i = 0; i < INVALID_ID_VALUES.length; i++) {
            if (INVALID_ID_VALUES[i].equals(str)) {
                return false;
            }
        }
        return true;
    }

    public static boolean checkAndroidSerial(String str) {
        if (str == null) {
            return false;
        }
        for (int i = 0; i < INVALID_ID_VALUES.length; i++) {
            if (INVALID_ID_VALUES[i].equals(str)) {
                return false;
            }
        }
        return (str.length() <= 3 || str.substring(0, 3).equals("***") || str.substring(0, 3).equals("000")) ? false : true;
    }

    public static final DeviceId getAdvertisingDeviceId(Context context) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        return b(context) ? getGooglePlayAdvertisingDeviceId(context) : getDeviceId(context);
    }

    public static final DeviceId getAndroidId(Context context) {
        String string;
        if (Integer.valueOf(Build.VERSION.SDK_INT).intValue() < 3 || (string = Settings.Secure.getString(context.getContentResolver(), "android_id")) == null || string.length() <= 0) {
            return null;
        }
        return new DeviceId("AndroidId", string, 7, 2);
    }

    public static final DeviceId getAndroidSerial() {
        if (Integer.valueOf(Build.VERSION.SDK_INT).intValue() >= 9) {
            return new DeviceId("AndroidSerial", API9.getAndroidSerial(), 3, 1);
        }
        return null;
    }

    public static DeviceId getDeviceId(Context context) {
        DeviceId androidSerial = getAndroidSerial();
        if (checkAndroidSerial(androidSerial.getId())) {
            return androidSerial;
        }
        DeviceId androidId = getAndroidId(context);
        if (checkAndroidId(androidId.getId())) {
            return androidId;
        }
        return null;
    }

    public static final DeviceId getGooglePlayAdvertisingDeviceId(Context context) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        try {
            if (isGooglePlayServicesAvailable(context)) {
                return new DeviceId(isAdvertisingIdEnabled(context) ? a(context) : NO_ID_AVAILABLE);
            }
            return null;
        } catch (IllegalStateException e) {
            throw e;
        }
    }

    public static boolean isAdvertisingIdEnabled(Context context) {
        boolean z = false;
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        if (!c) {
            b bVar = new b();
            Intent intent = new Intent(b);
            intent.setPackage(a);
            if (context.bindService(intent, bVar, 1)) {
                try {
                    z = new c(bVar.getBinder()).isLimitAdTrackingEnabled(true) ? false : true;
                } catch (Exception e) {
                } finally {
                    context.unbindService(bVar);
                }
            }
            if (!z) {
                c = true;
            }
        }
        return z;
    }

    public static boolean isGooglePlayServicesAvailable(Context context) {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Cannot be called from the main thread");
        }
        if (Build.VERSION.SDK_INT <= 8) {
            return false;
        }
        try {
            context.getPackageManager().getPackageInfo("com.android.vending", 0);
            b bVar = new b();
            Intent intent = new Intent(b);
            intent.setPackage(a);
            if (!context.bindService(intent, bVar, 1)) {
                return false;
            }
            context.unbindService(bVar);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

    public static String md5(String str) {
        try {
            byte[] bArrDigest = MessageDigest.getInstance("MD5").digest(str.getBytes(HTTP.UTF_8));
            StringBuilder sb = new StringBuilder(bArrDigest.length * 2);
            for (byte b2 : bArrDigest) {
                int i = b2 & 255;
                if (i < 16) {
                    sb.append('0');
                }
                sb.append(Integer.toHexString(i));
            }
            return sb.toString();
        } catch (UnsupportedEncodingException e) {
            throw new RuntimeException("Huh, UTF-8 should be supported?", e);
        } catch (NoSuchAlgorithmException e2) {
            throw new RuntimeException("Huh, MD5 should be supported?", e2);
        }
    }
}
