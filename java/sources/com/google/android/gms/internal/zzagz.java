package com.google.android.gms.internal;

import android.R;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.ActivityManager;
import android.app.AlertDialog;
import android.app.KeyguardManager;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.ApplicationInfo;
import android.content.pm.ResolveInfo;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;
import android.media.AudioManager;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Debug;
import android.os.Handler;
import android.os.Looper;
import android.os.PowerManager;
import android.os.Process;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.view.MotionEventCompat;
import android.text.TextUtils;
import android.util.DisplayMetrics;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.view.ViewTreeObserver;
import android.view.Window;
import android.view.WindowManager;
import android.webkit.WebView;
import android.widget.AdapterView;
import android.widget.PopupWindow;
import com.comscore.streaming.Constants;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import com.google.android.gms.ads.AdActivity;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.descriptors.com.google.android.gms.ads.dynamite.ModuleDescriptor;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigInteger;
import java.net.HttpURLConnection;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.atomic.AtomicReference;
import org.apache.http.protocol.HTTP;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzagz {
    public static final Handler zzZr = new zzafs(Looper.getMainLooper());
    private static AtomicReference<List<String>> zzZs = new AtomicReference<>(null);
    private static AtomicReference<List<String>> zzZt = new AtomicReference<>(null);
    private String zzJP;
    private com.google.android.gms.ads.internal.js.zzl zzLG;
    private final Object mLock = new Object();
    private boolean zzZu = true;
    private boolean zzZv = false;

    public static void runOnUiThread(Runnable runnable) {
        if (Looper.getMainLooper().getThread() == Thread.currentThread()) {
            runnable.run();
        } else {
            zzZr.post(runnable);
        }
    }

    public static boolean zzD(Context context) {
        boolean z;
        Intent intent = new Intent();
        intent.setClassName(context, AdActivity.CLASS_NAME);
        ResolveInfo resolveInfoResolveActivity = context.getPackageManager().resolveActivity(intent, 65536);
        if (resolveInfoResolveActivity == null || resolveInfoResolveActivity.activityInfo == null) {
            zzafr.zzaT("Could not find com.google.android.gms.ads.AdActivity, please make sure it is declared in AndroidManifest.xml.");
            return false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 16) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "keyboard"));
            z = false;
        } else {
            z = true;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 32) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "keyboardHidden"));
            z = false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 128) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "orientation"));
            z = false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 256) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "screenLayout"));
            z = false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 512) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "uiMode"));
            z = false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 1024) == 0) {
            zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "screenSize"));
            z = false;
        }
        if ((resolveInfoResolveActivity.activityInfo.configChanges & 2048) != 0) {
            return z;
        }
        zzafr.zzaT(String.format("com.google.android.gms.ads.AdActivity requires the android:configChanges value to contain \"%s\".", "smallestScreenSize"));
        return false;
    }

    protected static String zzF(Context context) {
        try {
            return new WebView(context).getSettings().getUserAgentString();
        } catch (Exception e) {
            return zzhN();
        }
    }

    public static AlertDialog.Builder zzG(Context context) {
        return new AlertDialog.Builder(context);
    }

    public static zzlz zzH(Context context) {
        return new zzlz(context);
    }

    private static String zzI(Context context) {
        ActivityManager.RunningTaskInfo runningTaskInfo;
        try {
            ActivityManager activityManager = (ActivityManager) context.getSystemService("activity");
            if (activityManager == null) {
                return null;
            }
            List<ActivityManager.RunningTaskInfo> runningTasks = activityManager.getRunningTasks(1);
            if (runningTasks != null && !runningTasks.isEmpty() && (runningTaskInfo = runningTasks.get(0)) != null && runningTaskInfo.topActivity != null) {
                return runningTaskInfo.topActivity.getClassName();
            }
        } catch (Exception e) {
        }
        return null;
    }

    public static boolean zzJ(Context context) {
        try {
            ActivityManager activityManager = (ActivityManager) context.getSystemService("activity");
            KeyguardManager keyguardManager = (KeyguardManager) context.getSystemService("keyguard");
            if (activityManager == null || keyguardManager == null) {
                return false;
            }
            List<ActivityManager.RunningAppProcessInfo> runningAppProcesses = activityManager.getRunningAppProcesses();
            if (runningAppProcesses == null) {
                return false;
            }
            for (ActivityManager.RunningAppProcessInfo runningAppProcessInfo : runningAppProcesses) {
                if (Process.myPid() == runningAppProcessInfo.pid) {
                    if (runningAppProcessInfo.importance != 100 || keyguardManager.inKeyguardRestrictedInputMode()) {
                        break;
                        break;
                    }
                    PowerManager powerManager = (PowerManager) context.getSystemService("power");
                    if (!(powerManager == null ? false : powerManager.isScreenOn())) {
                        break;
                    }
                    return true;
                }
            }
            return false;
        } catch (Throwable th) {
            return false;
        }
    }

    /* JADX WARN: Code restructure failed: missing block: B:18:0x002c, code lost:
    
        r0 = null;
     */
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public static android.graphics.Bitmap zzK(android.content.Context r3) {
        /*
            r1 = 0
            boolean r0 = r3 instanceof android.app.Activity
            if (r0 != 0) goto L6
        L5:
            return r1
        L6:
            com.google.android.gms.internal.zzme<java.lang.Boolean> r0 = com.google.android.gms.internal.zzmo.zzFi     // Catch: java.lang.RuntimeException -> L3d
            com.google.android.gms.internal.zzmm r2 = com.google.android.gms.ads.internal.zzbs.zzbL()     // Catch: java.lang.RuntimeException -> L3d
            java.lang.Object r0 = r2.zzd(r0)     // Catch: java.lang.RuntimeException -> L3d
            java.lang.Boolean r0 = (java.lang.Boolean) r0     // Catch: java.lang.RuntimeException -> L3d
            boolean r0 = r0.booleanValue()     // Catch: java.lang.RuntimeException -> L3d
            if (r0 == 0) goto L2e
            android.app.Activity r3 = (android.app.Activity) r3     // Catch: java.lang.RuntimeException -> L3d
            android.view.Window r0 = r3.getWindow()     // Catch: java.lang.RuntimeException -> L3d
            if (r0 == 0) goto L43
            android.view.View r0 = r0.getDecorView()     // Catch: java.lang.RuntimeException -> L3d
            android.view.View r0 = r0.getRootView()     // Catch: java.lang.RuntimeException -> L3d
            android.graphics.Bitmap r0 = zzo(r0)     // Catch: java.lang.RuntimeException -> L3d
        L2c:
            r1 = r0
            goto L5
        L2e:
            android.app.Activity r3 = (android.app.Activity) r3     // Catch: java.lang.RuntimeException -> L3d
            android.view.Window r0 = r3.getWindow()     // Catch: java.lang.RuntimeException -> L3d
            android.view.View r0 = r0.getDecorView()     // Catch: java.lang.RuntimeException -> L3d
            android.graphics.Bitmap r0 = zzn(r0)     // Catch: java.lang.RuntimeException -> L3d
            goto L2c
        L3d:
            r0 = move-exception
            java.lang.String r2 = "Fail to capture screen shot"
            com.google.android.gms.internal.zzafr.zzb(r2, r0)
        L43:
            r0 = r1
            goto L2c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzagz.zzK(android.content.Context):android.graphics.Bitmap");
    }

    public static AudioManager zzL(Context context) {
        return (AudioManager) context.getSystemService("audio");
    }

    public static float zzM(Context context) {
        AudioManager audioManagerZzL = zzL(context);
        if (audioManagerZzL == null) {
            return 0.0f;
        }
        int streamMaxVolume = audioManagerZzL.getStreamMaxVolume(3);
        int streamVolume = audioManagerZzL.getStreamVolume(3);
        if (streamMaxVolume != 0) {
            return streamVolume / streamMaxVolume;
        }
        return 0.0f;
    }

    public static int zzN(Context context) {
        ApplicationInfo applicationInfo = context.getApplicationInfo();
        if (applicationInfo == null) {
            return 0;
        }
        return applicationInfo.targetSdkVersion;
    }

    public static boolean zzO(Context context) {
        try {
            context.getClassLoader().loadClass("com.google.android.gms.ads.internal.ClientApi");
            return false;
        } catch (ClassNotFoundException e) {
            return true;
        } catch (Throwable th) {
            zzafr.zzb("Error loading class.", th);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "AdUtil.isLiteSdk");
            return false;
        }
    }

    public static int zzP(Context context) {
        return DynamiteModule.zzF(context, ModuleDescriptor.MODULE_ID);
    }

    public static int zzQ(Context context) {
        return DynamiteModule.zzE(context, ModuleDescriptor.MODULE_ID);
    }

    public static Bundle zza(zzgz zzgzVar) {
        String str;
        String strZzhu;
        String strZzhv;
        if (zzgzVar == null) {
            return null;
        }
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCZ)).booleanValue()) {
            if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDb)).booleanValue()) {
                return null;
            }
        }
        if (com.google.android.gms.ads.internal.zzbs.zzbD().zzhn() && com.google.android.gms.ads.internal.zzbs.zzbD().zzho()) {
            return null;
        }
        if (zzgzVar.zzcQ()) {
            zzgzVar.wakeup();
        }
        zzgt zzgtVarZzcO = zzgzVar.zzcO();
        if (zzgtVarZzcO != null) {
            String strZzcD = zzgtVarZzcO.zzcD();
            String strZzcE = zzgtVarZzcO.zzcE();
            String strZzcF = zzgtVarZzcO.zzcF();
            if (strZzcD != null) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zzaF(strZzcD);
            }
            if (strZzcF != null) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zzaG(strZzcF);
                strZzhu = strZzcD;
                str = strZzcE;
                strZzhv = strZzcF;
            } else {
                strZzhu = strZzcD;
                str = strZzcE;
                strZzhv = strZzcF;
            }
        } else {
            str = null;
            strZzhu = com.google.android.gms.ads.internal.zzbs.zzbD().zzhu();
            strZzhv = com.google.android.gms.ads.internal.zzbs.zzbD().zzhv();
        }
        Bundle bundle = new Bundle(1);
        if (strZzhv != null) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDb)).booleanValue() && !com.google.android.gms.ads.internal.zzbs.zzbD().zzho()) {
                bundle.putString("v_fp_vertical", strZzhv);
            }
        }
        if (strZzhu != null) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCZ)).booleanValue() && !com.google.android.gms.ads.internal.zzbs.zzbD().zzhn()) {
                bundle.putString("fingerprint", strZzhu);
                if (!strZzhu.equals(str)) {
                    bundle.putString("v_fp", str);
                }
            }
        }
        if (bundle.isEmpty()) {
            return null;
        }
        return bundle;
    }

    public static DisplayMetrics zza(WindowManager windowManager) {
        DisplayMetrics displayMetrics = new DisplayMetrics();
        windowManager.getDefaultDisplay().getMetrics(displayMetrics);
        return displayMetrics;
    }

    public static PopupWindow zza(View view, int i, int i2, boolean z) {
        return new PopupWindow(view, i, i2, false);
    }

    public static String zza(Context context, View view, zziv zzivVar) {
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDr)).booleanValue()) {
            return null;
        }
        try {
            JSONObject jSONObject = new JSONObject();
            JSONObject jSONObject2 = new JSONObject();
            jSONObject2.put("width", zzivVar.width);
            jSONObject2.put("height", zzivVar.height);
            jSONObject.put("size", jSONObject2);
            jSONObject.put("activity", zzI(context));
            if (!zzivVar.zzAt) {
                JSONArray jSONArray = new JSONArray();
                while (view != null) {
                    Object parent = view.getParent();
                    if (parent != null) {
                        int iIndexOfChild = parent instanceof ViewGroup ? ((ViewGroup) parent).indexOfChild(view) : -1;
                        JSONObject jSONObject3 = new JSONObject();
                        jSONObject3.put("type", parent.getClass().getName());
                        jSONObject3.put("index_of_child", iIndexOfChild);
                        jSONArray.put(jSONObject3);
                    }
                    view = (parent == null || !(parent instanceof View)) ? null : (View) parent;
                }
                if (jSONArray.length() > 0) {
                    jSONObject.put("parents", jSONArray);
                }
            }
            return jSONObject.toString();
        } catch (JSONException e) {
            zzafr.zzc("Fail to get view hierarchy json", e);
            return null;
        }
    }

    private static String zza(Context context, zzcu zzcuVar, String str, View view) {
        if (zzcuVar == null) {
            return str;
        }
        try {
            Uri uriZza = Uri.parse(str);
            if (zzcuVar.zzd(uriZza)) {
                uriZza = zzcuVar.zza(uriZza, context, view);
            }
            return uriZza.toString();
        } catch (Exception e) {
            return str;
        }
    }

    public static String zza(InputStreamReader inputStreamReader) throws IOException {
        StringBuilder sb = new StringBuilder(8192);
        char[] cArr = new char[2048];
        while (true) {
            int i = inputStreamReader.read(cArr);
            if (i == -1) {
                return sb.toString();
            }
            sb.append(cArr, 0, i);
        }
    }

    private final JSONArray zza(Collection<?> collection) throws JSONException {
        JSONArray jSONArray = new JSONArray();
        Iterator<?> it = collection.iterator();
        while (it.hasNext()) {
            zza(jSONArray, it.next());
        }
        return jSONArray;
    }

    public static void zza(Activity activity, ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener) {
        Window window = activity.getWindow();
        if (window == null || window.getDecorView() == null || window.getDecorView().getViewTreeObserver() == null) {
            return;
        }
        window.getDecorView().getViewTreeObserver().addOnGlobalLayoutListener(onGlobalLayoutListener);
    }

    public static void zza(Activity activity, ViewTreeObserver.OnScrollChangedListener onScrollChangedListener) {
        Window window = activity.getWindow();
        if (window == null || window.getDecorView() == null || window.getDecorView().getViewTreeObserver() == null) {
            return;
        }
        window.getDecorView().getViewTreeObserver().addOnScrollChangedListener(onScrollChangedListener);
    }

    @TargetApi(18)
    public static void zza(Context context, Uri uri) {
        try {
            Intent intent = new Intent("android.intent.action.VIEW", uri);
            Bundle bundle = new Bundle();
            intent.putExtras(bundle);
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGo)).booleanValue()) {
                zzc(context, intent);
            }
            bundle.putString("com.android.browser.application_id", context.getPackageName());
            context.startActivity(intent);
            String strValueOf = String.valueOf(uri.toString());
            zzafr.zzaC(new StringBuilder(String.valueOf(strValueOf).length() + 26).append("Opening ").append(strValueOf).append(" in a new browser.").toString());
        } catch (ActivityNotFoundException e) {
            zzafr.zzb("No browser is found.", e);
        }
    }

    public static void zza(Context context, String str, List<String> list) {
        Iterator<String> it = list.iterator();
        while (it.hasNext()) {
            new zzaiq(context, str, it.next()).zzgp();
        }
    }

    public static void zza(List<String> list, String str) {
        Iterator<String> it = list.iterator();
        while (it.hasNext()) {
            new zzaiq(it.next(), str).zzgp();
        }
    }

    private final void zza(JSONArray jSONArray, Object obj) throws JSONException {
        if (obj instanceof Bundle) {
            jSONArray.put(zzg((Bundle) obj));
            return;
        }
        if (obj instanceof Map) {
            jSONArray.put(zzj((Map) obj));
            return;
        }
        if (obj instanceof Collection) {
            jSONArray.put(zza((Collection<?>) obj));
            return;
        }
        if (!(obj instanceof Object[])) {
            jSONArray.put(obj);
            return;
        }
        JSONArray jSONArray2 = new JSONArray();
        for (Object obj2 : (Object[]) obj) {
            zza(jSONArray2, obj2);
        }
        jSONArray.put(jSONArray2);
    }

    private final void zza(JSONObject jSONObject, String str, Object obj) throws JSONException {
        if (obj instanceof Bundle) {
            jSONObject.put(str, zzg((Bundle) obj));
            return;
        }
        if (obj instanceof Map) {
            jSONObject.put(str, zzj((Map) obj));
            return;
        }
        if (obj instanceof Collection) {
            if (str == null) {
                str = "null";
            }
            jSONObject.put(str, zza((Collection<?>) obj));
        } else if (obj instanceof Object[]) {
            jSONObject.put(str, zza(Arrays.asList((Object[]) obj)));
        } else {
            jSONObject.put(str, obj);
        }
    }

    @TargetApi(MotionEventCompat.AXIS_DISTANCE)
    public static boolean zza(Activity activity, Configuration configuration) {
        zzji.zzds();
        int iZzc = zzaiy.zzc(activity, configuration.screenHeightDp);
        int iZzc2 = zzaiy.zzc(activity, configuration.screenWidthDp);
        DisplayMetrics displayMetricsZza = zza((WindowManager) activity.getApplicationContext().getSystemService("window"));
        int i = displayMetricsZza.heightPixels;
        int i2 = displayMetricsZza.widthPixels;
        int identifier = activity.getResources().getIdentifier("status_bar_height", "dimen", Constants.C10_VALUE);
        int dimensionPixelSize = identifier > 0 ? activity.getResources().getDimensionPixelSize(identifier) : 0;
        int iIntValue = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGy)).intValue() * ((int) Math.round(((double) activity.getResources().getDisplayMetrics().density) + 0.5d));
        return zzb(i, dimensionPixelSize + iZzc, iIntValue) && zzb(i2, iZzc2, iIntValue);
    }

    public static boolean zza(ClassLoader classLoader, Class<?> cls, String str) {
        try {
            return cls.isAssignableFrom(Class.forName(str, false, classLoader));
        } catch (Throwable th) {
            return false;
        }
    }

    public static String zzaI(String str) {
        return Uri.parse(str).buildUpon().query(null).build().toString();
    }

    public static int zzaJ(String str) {
        try {
            return Integer.parseInt(str);
        } catch (NumberFormatException e) {
            String strValueOf = String.valueOf(e);
            zzafr.zzaT(new StringBuilder(String.valueOf(strValueOf).length() + 22).append("Could not parse value:").append(strValueOf).toString());
            return 0;
        }
    }

    public static boolean zzaK(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        return str.matches("([^\\s]+(\\.(?i)(jpg|png|gif|bmp|webp))$)");
    }

    public static boolean zzaL(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        if (zzZs.get() == null) {
            try {
                JSONArray jSONArray = new JSONArray((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDw));
                ArrayList arrayList = new ArrayList(jSONArray.length());
                for (int i = 0; i < jSONArray.length(); i++) {
                    arrayList.add(jSONArray.getString(i));
                }
                zzZs.compareAndSet(null, arrayList);
            } catch (JSONException e) {
                zzafr.zzaT("Could not parse click ping schema");
                return false;
            }
        }
        Iterator<String> it = zzZs.get().iterator();
        while (it.hasNext()) {
            if (str.contains(it.next())) {
                return true;
            }
        }
        return false;
    }

    public static boolean zzaM(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        if (zzZt.get() == null) {
            try {
                JSONArray jSONArray = new JSONArray((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDx));
                ArrayList arrayList = new ArrayList(jSONArray.length());
                for (int i = 0; i < jSONArray.length(); i++) {
                    arrayList.add(jSONArray.getString(i));
                }
                zzZt.compareAndSet(null, arrayList);
            } catch (JSONException e) {
                zzafr.zzaT("Could not parse impression ping schema");
                return false;
            }
        }
        Iterator<String> it = zzZt.get().iterator();
        while (it.hasNext()) {
            if (str.contains(it.next())) {
                return true;
            }
        }
        return false;
    }

    public static Uri zzb(String str, String str2, String str3) throws UnsupportedOperationException {
        int iIndexOf = str.indexOf("&adurl");
        if (iIndexOf == -1) {
            iIndexOf = str.indexOf("?adurl");
        }
        return iIndexOf != -1 ? Uri.parse(str.substring(0, iIndexOf + 1) + str2 + "=" + str3 + "&" + str.substring(iIndexOf + 1)) : Uri.parse(str).buildUpon().appendQueryParameter(str2, str3).build();
    }

    public static String zzb(zzaka zzakaVar, String str) {
        return zza(zzakaVar.getContext(), zzakaVar.zziy(), str, zzakaVar.getView());
    }

    public static void zzb(Activity activity, ViewTreeObserver.OnScrollChangedListener onScrollChangedListener) {
        Window window = activity.getWindow();
        if (window == null || window.getDecorView() == null || window.getDecorView().getViewTreeObserver() == null) {
            return;
        }
        window.getDecorView().getViewTreeObserver().removeOnScrollChangedListener(onScrollChangedListener);
    }

    public static void zzb(Context context, Intent intent) {
        try {
            context.startActivity(intent);
        } catch (Throwable th) {
            intent.addFlags(268435456);
            context.startActivity(intent);
        }
    }

    public static void zzb(Runnable runnable) {
        if (Looper.getMainLooper().getThread() != Thread.currentThread()) {
            runnable.run();
        } else {
            zzagt.zza(runnable);
        }
    }

    private static boolean zzb(int i, int i2, int i3) {
        return Math.abs(i - i2) <= i3;
    }

    public static float zzbf() {
        com.google.android.gms.ads.internal.zzbs.zzbT();
        com.google.android.gms.ads.internal.zzax zzaxVarZzbe = com.google.android.gms.ads.internal.zzax.zzbe();
        if (zzaxVarZzbe == null || !zzaxVarZzbe.zzbg()) {
            return 1.0f;
        }
        return zzaxVarZzbe.zzbf();
    }

    public static boolean zzbh() {
        com.google.android.gms.ads.internal.zzbs.zzbT();
        com.google.android.gms.ads.internal.zzax zzaxVarZzbe = com.google.android.gms.ads.internal.zzax.zzbe();
        if (zzaxVarZzbe != null) {
            return zzaxVarZzbe.zzbh();
        }
        return false;
    }

    @TargetApi(18)
    public static void zzc(Context context, Intent intent) {
        if (intent != null && com.google.android.gms.common.util.zzq.zzsb()) {
            Bundle extras = intent.getExtras() != null ? intent.getExtras() : new Bundle();
            extras.putBinder("android.support.customtabs.extra.SESSION", null);
            extras.putString("com.android.browser.application_id", context.getPackageName());
            intent.putExtras(extras);
        }
    }

    public static boolean zzc(Context context, String str, String str2) {
        return zzbha.zzaP(context).checkPermission(str2, str) == 0;
    }

    public static void zzd(Context context, String str, String str2) {
        ArrayList arrayList = new ArrayList();
        arrayList.add(str2);
        zza(context, str, arrayList);
    }

    public static void zze(Context context, String str, String str2) {
        try {
            FileOutputStream fileOutputStreamOpenFileOutput = context.openFileOutput(str, 0);
            fileOutputStreamOpenFileOutput.write(str2.getBytes(HTTP.UTF_8));
            fileOutputStreamOpenFileOutput.close();
        } catch (Exception e) {
            zzafr.zzb("Error writing to file in internal storage.", e);
        }
    }

    public static int[] zzf(Activity activity) {
        View viewFindViewById;
        Window window = activity.getWindow();
        return (window == null || (viewFindViewById = window.findViewById(R.id.content)) == null) ? zzhR() : new int[]{viewFindViewById.getWidth(), viewFindViewById.getHeight()};
    }

    public static Map<String, String> zzg(Uri uri) {
        if (uri == null) {
            return null;
        }
        HashMap map = new HashMap();
        for (String str : com.google.android.gms.ads.internal.zzbs.zzbB().zzh(uri)) {
            map.put(str, uri.getQueryParameter(str));
        }
        return map;
    }

    private final JSONObject zzg(Bundle bundle) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        for (String str : bundle.keySet()) {
            zza(jSONObject, str, bundle.get(str));
        }
        return jSONObject;
    }

    private static String zzhN() {
        StringBuffer stringBuffer = new StringBuffer(256);
        stringBuffer.append("Mozilla/5.0 (Linux; U; Android");
        if (Build.VERSION.RELEASE != null) {
            stringBuffer.append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(Build.VERSION.RELEASE);
        }
        stringBuffer.append("; ").append(Locale.getDefault());
        if (Build.DEVICE != null) {
            stringBuffer.append("; ").append(Build.DEVICE);
            if (Build.DISPLAY != null) {
                stringBuffer.append(" Build/").append(Build.DISPLAY);
            }
        }
        stringBuffer.append(") AppleWebKit/533 Version/4.0 Safari/533");
        return stringBuffer.toString();
    }

    public static String zzhO() {
        return UUID.randomUUID().toString();
    }

    public static String zzhP() {
        UUID uuidRandomUUID = UUID.randomUUID();
        byte[] byteArray = BigInteger.valueOf(uuidRandomUUID.getLeastSignificantBits()).toByteArray();
        byte[] byteArray2 = BigInteger.valueOf(uuidRandomUUID.getMostSignificantBits()).toByteArray();
        String string = new BigInteger(1, byteArray).toString();
        for (int i = 0; i < 2; i++) {
            try {
                MessageDigest messageDigest = MessageDigest.getInstance("MD5");
                messageDigest.update(byteArray);
                messageDigest.update(byteArray2);
                byte[] bArr = new byte[8];
                System.arraycopy(messageDigest.digest(), 0, bArr, 0, 8);
                string = new BigInteger(1, bArr).toString();
            } catch (NoSuchAlgorithmException e) {
            }
        }
        return string;
    }

    public static String zzhQ() {
        String str = Build.MANUFACTURER;
        String str2 = Build.MODEL;
        return str2.startsWith(str) ? str2 : new StringBuilder(String.valueOf(str).length() + 1 + String.valueOf(str2).length()).append(str).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(str2).toString();
    }

    private static int[] zzhR() {
        return new int[]{0, 0};
    }

    public static Bundle zzhS() {
        Bundle bundle = new Bundle();
        try {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCC)).booleanValue()) {
                Debug.MemoryInfo memoryInfo = new Debug.MemoryInfo();
                Debug.getMemoryInfo(memoryInfo);
                bundle.putParcelable("debug_memory_info", memoryInfo);
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCD)).booleanValue()) {
                Runtime runtime = Runtime.getRuntime();
                bundle.putLong("runtime_free_memory", runtime.freeMemory());
                bundle.putLong("runtime_max_memory", runtime.maxMemory());
                bundle.putLong("runtime_total_memory", runtime.totalMemory());
            }
            bundle.putInt("web_view_count", com.google.android.gms.ads.internal.zzbs.zzbD().zzhJ());
        } catch (Exception e) {
            zzafr.zzc("Unable to gather memory stats", e);
        }
        return bundle;
    }

    public static Bitmap zzl(View view) {
        view.setDrawingCacheEnabled(true);
        Bitmap bitmapCreateBitmap = Bitmap.createBitmap(view.getDrawingCache());
        view.setDrawingCacheEnabled(false);
        return bitmapCreateBitmap;
    }

    public static Bitmap zzm(View view) {
        if (view == null) {
            return null;
        }
        Bitmap bitmapZzo = zzo(view);
        return bitmapZzo == null ? zzn(view) : bitmapZzo;
    }

    private static Bitmap zzn(@NonNull View view) {
        Bitmap bitmapCreateBitmap;
        try {
            int width = view.getWidth();
            int height = view.getHeight();
            if (width == 0 || height == 0) {
                zzafr.zzaT("Width or height of view is zero");
                bitmapCreateBitmap = null;
            } else {
                bitmapCreateBitmap = Bitmap.createBitmap(view.getWidth(), view.getHeight(), Bitmap.Config.RGB_565);
                Canvas canvas = new Canvas(bitmapCreateBitmap);
                view.layout(0, 0, width, height);
                view.draw(canvas);
            }
            return bitmapCreateBitmap;
        } catch (RuntimeException e) {
            zzafr.zzb("Fail to capture the webview", e);
            return null;
        }
    }

    private static Bitmap zzo(@NonNull View view) {
        Bitmap bitmapCreateBitmap;
        RuntimeException e;
        try {
            boolean zIsDrawingCacheEnabled = view.isDrawingCacheEnabled();
            view.setDrawingCacheEnabled(true);
            Bitmap drawingCache = view.getDrawingCache();
            bitmapCreateBitmap = drawingCache != null ? Bitmap.createBitmap(drawingCache) : null;
            try {
                view.setDrawingCacheEnabled(zIsDrawingCacheEnabled);
            } catch (RuntimeException e2) {
                e = e2;
                zzafr.zzb("Fail to capture the web view", e);
            }
        } catch (RuntimeException e3) {
            bitmapCreateBitmap = null;
            e = e3;
        }
        return bitmapCreateBitmap;
    }

    public static int zzp(@Nullable View view) {
        if (view == null) {
            return -1;
        }
        ViewParent parent = view.getParent();
        while (parent != null && !(parent instanceof AdapterView)) {
            parent = parent.getParent();
        }
        if (parent == null) {
            return -1;
        }
        return ((AdapterView) parent).getPositionForView(view);
    }

    public static String zzt(Context context, String str) {
        try {
            return new String(com.google.android.gms.common.util.zzn.zza(context.openFileInput(str), true), HTTP.UTF_8);
        } catch (IOException e) {
            zzafr.zzaC("Error reading from internal storage.");
            return "";
        }
    }

    public final boolean zzE(Context context) {
        if (this.zzZv) {
            return false;
        }
        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction("android.intent.action.USER_PRESENT");
        intentFilter.addAction("android.intent.action.SCREEN_OFF");
        context.getApplicationContext().registerReceiver(new zzahd(this, null), intentFilter);
        this.zzZv = true;
        return true;
    }

    public final JSONObject zza(Bundle bundle, JSONObject jSONObject) {
        try {
            return zzg(bundle);
        } catch (JSONException e) {
            zzafr.zzb("Error converting Bundle to JSON", e);
            return null;
        }
    }

    public final void zza(Context context, @Nullable String str, String str2, Bundle bundle, boolean z) {
        if (z) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            bundle.putString("device", zzhQ());
            bundle.putString("eids", TextUtils.join(",", zzmo.zzdJ()));
        }
        zzji.zzds();
        zzaiy.zza(context, str, str2, bundle, z, new zzahc(this, context, str));
    }

    public final void zza(Context context, String str, boolean z, HttpURLConnection httpURLConnection) {
        httpURLConnection.setConnectTimeout(com.comscore.utils.Constants.MINIMAL_AUTOUPDATE_INTERVAL);
        httpURLConnection.setInstanceFollowRedirects(z);
        httpURLConnection.setReadTimeout(com.comscore.utils.Constants.MINIMAL_AUTOUPDATE_INTERVAL);
        httpURLConnection.setRequestProperty(HTTP.USER_AGENT, zzs(context, str));
        httpURLConnection.setUseCaches(false);
    }

    public final void zza(Context context, List<String> list) {
        if ((context instanceof Activity) && !TextUtils.isEmpty(aeo.zzbU((Activity) context))) {
            if (list == null) {
                zzafr.v("Cannot ping urls: empty list.");
            } else {
                if (!zznl.zzi(context)) {
                    zzafr.v("Cannot ping url because custom tabs is not supported");
                    return;
                }
                zznl zznlVar = new zznl();
                zznlVar.zza(new zzaha(this, list, zznlVar, context));
                zznlVar.zzd((Activity) context);
            }
        }
    }

    public final boolean zza(View view, Context context) {
        KeyguardManager keyguardManager = null;
        Context applicationContext = context.getApplicationContext();
        PowerManager powerManager = applicationContext != null ? (PowerManager) applicationContext.getSystemService("power") : null;
        Object systemService = context.getSystemService("keyguard");
        if (systemService != null && (systemService instanceof KeyguardManager)) {
            keyguardManager = (KeyguardManager) systemService;
        }
        return zza(view, powerManager, keyguardManager);
    }

    /* JADX WARN: Code duplicated, block: B:23:0x0047  */
    /* JADX WARN: Code duplicated, block: B:42:0x008f  */
    /* JADX WARN: Code duplicated, block: B:44:0x0096  */
    public final boolean zza(View view, PowerManager powerManager, KeyguardManager keyguardManager) {
        boolean z;
        Activity activity;
        boolean z2;
        if (com.google.android.gms.ads.internal.zzbs.zzbz().zzZu) {
            z = true;
        } else {
            if (keyguardManager == null ? false : keyguardManager.inKeyguardRestrictedInputMode()) {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEs)).booleanValue()) {
                    View rootView = view.getRootView();
                    if (rootView != null) {
                        Context context = rootView.getContext();
                        if (context instanceof Activity) {
                            activity = (Activity) context;
                        } else {
                            activity = null;
                        }
                    } else {
                        activity = null;
                    }
                    if (activity == null) {
                        z2 = false;
                    } else {
                        Window window = activity.getWindow();
                        WindowManager.LayoutParams attributes = window == null ? null : window.getAttributes();
                        if (attributes == null || (attributes.flags & 524288) == 0) {
                            z2 = false;
                        } else {
                            z2 = true;
                        }
                    }
                    if (z2) {
                        z = true;
                    }
                }
                z = false;
            } else {
                z = true;
            }
        }
        if (view.getVisibility() == 0 && view.isShown()) {
            if ((powerManager == null || powerManager.isScreenOn()) && z) {
                if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEq)).booleanValue() || view.getLocalVisibleRect(new Rect()) || view.getGlobalVisibleRect(new Rect())) {
                    return true;
                }
            }
        }
        return false;
    }

    public final void zzb(Context context, String str, String str2, Bundle bundle, boolean z) {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEv)).booleanValue()) {
            zza(context, str, str2, bundle, z);
        }
    }

    public final com.google.android.gms.ads.internal.js.zzl zze(Context context, zzaje zzajeVar) {
        com.google.android.gms.ads.internal.js.zzl zzlVar;
        synchronized (this.mLock) {
            if (this.zzLG == null) {
                if (context.getApplicationContext() != null) {
                    context = context.getApplicationContext();
                }
                this.zzLG = new com.google.android.gms.ads.internal.js.zzl(context, zzajeVar, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzBX));
            }
            zzlVar = this.zzLG;
        }
        return zzlVar;
    }

    public final int[] zzg(Activity activity) {
        int[] iArrZzf = zzf(activity);
        zzji.zzds();
        zzji.zzds();
        return new int[]{zzaiy.zzd(activity, iArrZzf[0]), zzaiy.zzd(activity, iArrZzf[1])};
    }

    public final int[] zzh(Activity activity) {
        View viewFindViewById;
        Window window = activity.getWindow();
        int[] iArrZzhR = (window == null || (viewFindViewById = window.findViewById(R.id.content)) == null) ? zzhR() : new int[]{viewFindViewById.getTop(), viewFindViewById.getBottom()};
        zzji.zzds();
        zzji.zzds();
        return new int[]{zzaiy.zzd(activity, iArrZzhR[0]), zzaiy.zzd(activity, iArrZzhR[1])};
    }

    public final JSONObject zzj(Map<String, ?> map) throws JSONException {
        try {
            JSONObject jSONObject = new JSONObject();
            for (String str : map.keySet()) {
                zza(jSONObject, str, map.get(str));
            }
            return jSONObject;
        } catch (ClassCastException e) {
            String strValueOf = String.valueOf(e.getMessage());
            throw new JSONException(strValueOf.length() != 0 ? "Could not convert map to JSON: ".concat(strValueOf) : new String("Could not convert map to JSON: "));
        }
    }

    public final String zzs(Context context, String str) {
        String strZzhN;
        synchronized (this.mLock) {
            if (this.zzJP != null) {
                strZzhN = this.zzJP;
            } else if (str == null) {
                strZzhN = zzhN();
            } else {
                try {
                    this.zzJP = com.google.android.gms.ads.internal.zzbs.zzbB().getDefaultUserAgent(context);
                } catch (Exception e) {
                }
                if (TextUtils.isEmpty(this.zzJP)) {
                    zzji.zzds();
                    if (zzaiy.zzil()) {
                        this.zzJP = zzF(context);
                    } else {
                        this.zzJP = null;
                        zzZr.post(new zzahb(this, context));
                        while (this.zzJP == null) {
                            try {
                                this.mLock.wait();
                            } catch (InterruptedException e2) {
                                this.zzJP = zzhN();
                                String strValueOf = String.valueOf(this.zzJP);
                                zzafr.zzaT(strValueOf.length() != 0 ? "Interrupted, use default user agent: ".concat(strValueOf) : new String("Interrupted, use default user agent: "));
                            }
                        }
                    }
                }
                String strValueOf2 = String.valueOf(this.zzJP);
                this.zzJP = new StringBuilder(String.valueOf(strValueOf2).length() + 11 + String.valueOf(str).length()).append(strValueOf2).append(" (Mobile; ").append(str).append(")").toString();
                strZzhN = this.zzJP;
            }
        }
        return strZzhN;
    }
}
