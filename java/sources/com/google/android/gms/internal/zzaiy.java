package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.ContentResolver;
import android.content.Context;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.internal.view.SupportMenu;
import android.support.v4.view.ViewCompat;
import android.util.DisplayMetrics;
import android.util.TypedValue;
import android.view.Display;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.TextView;
import com.adobe.mobile.TargetLocationRequest;
import com.comscore.utils.Constants;
import com.google.android.gms.ads.AdLoader;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.InterstitialAd;
import com.google.android.gms.ads.doubleclick.PublisherAdView;
import com.google.android.gms.ads.doubleclick.PublisherInterstitialAd;
import com.google.android.gms.ads.search.SearchAdView;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.descriptors.com.google.android.gms.ads.dynamite.ModuleDescriptor;
import java.math.BigInteger;
import java.net.HttpURLConnection;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Locale;
import java.util.StringTokenizer;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaiy {
    public static final Handler zzaaH = new Handler(Looper.getMainLooper());
    private static final String zzaaI = AdView.class.getName();
    private static final String zzaaJ = InterstitialAd.class.getName();
    private static final String zzaaK = PublisherAdView.class.getName();
    private static final String zzaaL = PublisherInterstitialAd.class.getName();
    private static final String zzaaM = SearchAdView.class.getName();
    private static final String zzaaN = AdLoader.class.getName();

    public static int zzP(Context context) {
        return DynamiteModule.zzF(context, ModuleDescriptor.MODULE_ID);
    }

    public static int zzQ(Context context) {
        return DynamiteModule.zzE(context, ModuleDescriptor.MODULE_ID);
    }

    public static String zzV(Context context) {
        ContentResolver contentResolver = context.getContentResolver();
        String string = contentResolver == null ? null : Settings.Secure.getString(contentResolver, "android_id");
        if (string == null || zzik()) {
            string = "emulator";
        }
        return zzaR(string);
    }

    @Nullable
    public static String zzW(Context context) {
        ContentResolver contentResolver = context.getContentResolver();
        if (contentResolver == null) {
            return null;
        }
        return Settings.Secure.getString(contentResolver, "android_id");
    }

    public static boolean zzX(Context context) {
        return com.google.android.gms.common.zze.zzoW().isGooglePlayServicesAvailable(context) == 0;
    }

    public static boolean zzY(Context context) {
        int iIsGooglePlayServicesAvailable = com.google.android.gms.common.zze.zzoW().isGooglePlayServicesAvailable(context);
        return iIsGooglePlayServicesAvailable == 0 || iIsGooglePlayServicesAvailable == 2;
    }

    public static boolean zzZ(Context context) {
        if (context.getResources().getConfiguration().orientation != 2) {
            return false;
        }
        DisplayMetrics displayMetrics = context.getResources().getDisplayMetrics();
        return ((int) (((float) displayMetrics.heightPixels) / displayMetrics.density)) < 600;
    }

    public static int zza(DisplayMetrics displayMetrics, int i) {
        return (int) TypedValue.applyDimension(1, i, displayMetrics);
    }

    @Nullable
    public static String zza(StackTraceElement[] stackTraceElementArr, String str) {
        String className;
        int i = 0;
        while (true) {
            if (i + 1 >= stackTraceElementArr.length) {
                className = null;
                break;
            }
            StackTraceElement stackTraceElement = stackTraceElementArr[i];
            String className2 = stackTraceElement.getClassName();
            if ("loadAd".equalsIgnoreCase(stackTraceElement.getMethodName()) && (zzaaI.equalsIgnoreCase(className2) || zzaaJ.equalsIgnoreCase(className2) || zzaaK.equalsIgnoreCase(className2) || zzaaL.equalsIgnoreCase(className2) || zzaaM.equalsIgnoreCase(className2) || zzaaN.equalsIgnoreCase(className2))) {
                className = stackTraceElementArr[i + 1].getClassName();
                break;
            }
            i++;
        }
        if (str != null) {
            StringTokenizer stringTokenizer = new StringTokenizer(str, ".");
            StringBuilder sb = new StringBuilder();
            int i2 = 2;
            if (stringTokenizer.hasMoreElements()) {
                sb.append(stringTokenizer.nextToken());
                while (true) {
                    int i3 = i2 - 1;
                    if (i2 <= 0 || !stringTokenizer.hasMoreElements()) {
                        break;
                    }
                    sb.append(".").append(stringTokenizer.nextToken());
                    i2 = i3;
                }
                str = sb.toString();
            }
            if (className != null && !className.contains(str)) {
                return className;
            }
        }
        return null;
    }

    public static void zza(Context context, @Nullable String str, String str2, Bundle bundle, boolean z, zzajb zzajbVar) {
        if (z) {
            Context applicationContext = context.getApplicationContext();
            if (applicationContext == null) {
                applicationContext = context;
            }
            bundle.putString("os", Build.VERSION.RELEASE);
            bundle.putString("api", String.valueOf(Build.VERSION.SDK_INT));
            bundle.putString("appid", applicationContext.getPackageName());
            if (str == null) {
                com.google.android.gms.common.zze.zzoW();
                str = new StringBuilder(23).append(com.google.android.gms.common.zze.zzau(context)).append(".11020000").toString();
            }
            bundle.putString("js", str);
        }
        Uri.Builder builderAppendQueryParameter = new Uri.Builder().scheme("https").path("//pagead2.googlesyndication.com/pagead/gen_204").appendQueryParameter(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, str2);
        for (String str3 : bundle.keySet()) {
            builderAppendQueryParameter.appendQueryParameter(str3, bundle.getString(str3));
        }
        zzajbVar.zzaN(builderAppendQueryParameter.toString());
    }

    private final void zza(ViewGroup viewGroup, zziv zzivVar, String str, int i, int i2) {
        if (viewGroup.getChildCount() != 0) {
            return;
        }
        Context context = viewGroup.getContext();
        TextView textView = new TextView(context);
        textView.setGravity(17);
        textView.setText(str);
        textView.setTextColor(i);
        textView.setBackgroundColor(i2);
        FrameLayout frameLayout = new FrameLayout(context);
        frameLayout.setBackgroundColor(i);
        int iZzc = zzc(context, 3);
        frameLayout.addView(textView, new FrameLayout.LayoutParams(zzivVar.widthPixels - iZzc, zzivVar.heightPixels - iZzc, 17));
        viewGroup.addView(frameLayout, zzivVar.widthPixels, zzivVar.heightPixels);
    }

    public static void zza(boolean z, HttpURLConnection httpURLConnection, @Nullable String str) {
        httpURLConnection.setConnectTimeout(Constants.MINIMAL_AUTOUPDATE_INTERVAL);
        httpURLConnection.setInstanceFollowRedirects(true);
        httpURLConnection.setReadTimeout(Constants.MINIMAL_AUTOUPDATE_INTERVAL);
        if (str != null) {
            httpURLConnection.setRequestProperty(HTTP.USER_AGENT, str);
        }
        httpURLConnection.setUseCaches(false);
    }

    public static String zzaR(String str) {
        for (int i = 0; i < 2; i++) {
            try {
                MessageDigest messageDigest = MessageDigest.getInstance("MD5");
                messageDigest.update(str.getBytes());
                return String.format(Locale.US, "%032X", new BigInteger(1, messageDigest.digest()));
            } catch (NoSuchAlgorithmException e) {
            }
        }
        return null;
    }

    @TargetApi(17)
    public static boolean zzaa(Context context) {
        int iIntValue;
        int iIntValue2;
        DisplayMetrics displayMetrics = context.getResources().getDisplayMetrics();
        Display defaultDisplay = ((WindowManager) context.getSystemService("window")).getDefaultDisplay();
        if (com.google.android.gms.common.util.zzq.zzsa()) {
            defaultDisplay.getRealMetrics(displayMetrics);
            iIntValue = displayMetrics.heightPixels;
            iIntValue2 = displayMetrics.widthPixels;
        } else {
            try {
                iIntValue = ((Integer) Display.class.getMethod("getRawHeight", new Class[0]).invoke(defaultDisplay, new Object[0])).intValue();
                iIntValue2 = ((Integer) Display.class.getMethod("getRawWidth", new Class[0]).invoke(defaultDisplay, new Object[0])).intValue();
            } catch (Exception e) {
                return false;
            }
        }
        defaultDisplay.getMetrics(displayMetrics);
        return displayMetrics.heightPixels == iIntValue && displayMetrics.widthPixels == iIntValue2;
    }

    public static int zzab(Context context) {
        int identifier = context.getResources().getIdentifier("navigation_bar_width", "dimen", com.comscore.streaming.Constants.C10_VALUE);
        if (identifier > 0) {
            return context.getResources().getDimensionPixelSize(identifier);
        }
        return 0;
    }

    public static int zzb(DisplayMetrics displayMetrics, int i) {
        return Math.round(i / displayMetrics.density);
    }

    public static int zzc(Context context, int i) {
        return zza(context.getResources().getDisplayMetrics(), i);
    }

    public static int zzd(Context context, int i) {
        Display defaultDisplay = ((WindowManager) context.getSystemService("window")).getDefaultDisplay();
        DisplayMetrics displayMetrics = new DisplayMetrics();
        defaultDisplay.getMetrics(displayMetrics);
        return zzb(displayMetrics, i);
    }

    public static boolean zzik() {
        return Build.DEVICE.startsWith("generic");
    }

    public static boolean zzil() {
        return Looper.myLooper() == Looper.getMainLooper();
    }

    public final void zza(Context context, @Nullable String str, String str2, Bundle bundle, boolean z) {
        zza(context, null, str2, bundle, true, new zzaiz(this));
    }

    public final void zza(ViewGroup viewGroup, zziv zzivVar, String str) {
        zza(viewGroup, zzivVar, str, ViewCompat.MEASURED_STATE_MASK, -1);
    }

    public final void zza(ViewGroup viewGroup, zziv zzivVar, String str, String str2) {
        zzajc.zzaT(str2);
        zza(viewGroup, zzivVar, str, SupportMenu.CATEGORY_MASK, ViewCompat.MEASURED_STATE_MASK);
    }
}
