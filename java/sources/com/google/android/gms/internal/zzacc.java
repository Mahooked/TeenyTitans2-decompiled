package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.content.res.Resources;
import android.media.AudioManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Build;
import android.telephony.TelephonyManager;
import android.util.DisplayMetrics;
import java.util.Locale;

/* JADX INFO: loaded from: classes.dex */
public final class zzacc {
    private int zzSI;
    private int zzSJ;
    private int zzVF;
    private boolean zzVG;
    private boolean zzVH;
    private String zzVI;
    private String zzVJ;
    private boolean zzVK;
    private boolean zzVL;
    private boolean zzVM;
    private boolean zzVN;
    private String zzVO;
    private String zzVP;
    private String zzVQ;
    private int zzVR;
    private int zzVS;
    private int zzVT;
    private int zzVU;
    private int zzVV;
    private int zzVW;
    private double zzVX;
    private boolean zzVY;
    private boolean zzVZ;
    private int zzWa;
    private String zzWb;
    private String zzWc;
    private boolean zzWd;
    private float zzxR;

    public zzacc(Context context) {
        DisplayMetrics displayMetrics;
        PackageManager packageManager = context.getPackageManager();
        zzk(context);
        zzl(context);
        zzm(context);
        Locale locale = Locale.getDefault();
        this.zzVG = zza(packageManager, "geo:0,0?q=donuts") != null;
        this.zzVH = zza(packageManager, "http://www.google.com") != null;
        this.zzVJ = locale.getCountry();
        zzji.zzds();
        this.zzVK = zzaiy.zzik();
        this.zzVL = com.google.android.gms.common.util.zzj.zzaI(context);
        this.zzVO = locale.getLanguage();
        this.zzVP = zzb(context, packageManager);
        this.zzVQ = zza(context, packageManager);
        Resources resources = context.getResources();
        if (resources == null || (displayMetrics = resources.getDisplayMetrics()) == null) {
            return;
        }
        this.zzxR = displayMetrics.density;
        this.zzSI = displayMetrics.widthPixels;
        this.zzSJ = displayMetrics.heightPixels;
    }

    public zzacc(Context context, zzacb zzacbVar) {
        context.getPackageManager();
        zzk(context);
        zzl(context);
        zzm(context);
        this.zzWb = Build.FINGERPRINT;
        this.zzWc = Build.DEVICE;
        this.zzWd = zznl.zzi(context);
        this.zzVG = zzacbVar.zzVG;
        this.zzVH = zzacbVar.zzVH;
        this.zzVJ = zzacbVar.zzVJ;
        this.zzVK = zzacbVar.zzVK;
        this.zzVL = zzacbVar.zzVL;
        this.zzVO = zzacbVar.zzVO;
        this.zzVP = zzacbVar.zzVP;
        this.zzVQ = zzacbVar.zzVQ;
        this.zzxR = zzacbVar.zzxR;
        this.zzSI = zzacbVar.zzSI;
        this.zzSJ = zzacbVar.zzSJ;
    }

    private static ResolveInfo zza(PackageManager packageManager, String str) {
        return packageManager.resolveActivity(new Intent("android.intent.action.VIEW", Uri.parse(str)), 65536);
    }

    private static String zza(Context context, PackageManager packageManager) {
        try {
            PackageInfo packageInfo = zzbha.zzaP(context).getPackageInfo("com.android.vending", 128);
            if (packageInfo == null) {
                return null;
            }
            int i = packageInfo.versionCode;
            String strValueOf = String.valueOf(packageInfo.packageName);
            return new StringBuilder(String.valueOf(strValueOf).length() + 12).append(i).append(".").append(strValueOf).toString();
        } catch (Exception e) {
            return null;
        }
    }

    private static String zzb(Context context, PackageManager packageManager) {
        ActivityInfo activityInfo;
        ResolveInfo resolveInfoZza = zza(packageManager, "market://details?id=com.google.android.gms.ads");
        if (resolveInfoZza == null || (activityInfo = resolveInfoZza.activityInfo) == null) {
            return null;
        }
        try {
            PackageInfo packageInfo = zzbha.zzaP(context).getPackageInfo(activityInfo.packageName, 0);
            if (packageInfo == null) {
                return null;
            }
            int i = packageInfo.versionCode;
            String strValueOf = String.valueOf(activityInfo.packageName);
            return new StringBuilder(String.valueOf(strValueOf).length() + 12).append(i).append(".").append(strValueOf).toString();
        } catch (PackageManager.NameNotFoundException e) {
            return null;
        }
    }

    private final void zzk(Context context) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        AudioManager audioManagerZzL = zzagz.zzL(context);
        if (audioManagerZzL != null) {
            try {
                this.zzVF = audioManagerZzL.getMode();
                this.zzVM = audioManagerZzL.isMusicActive();
                this.zzVN = audioManagerZzL.isSpeakerphoneOn();
                this.zzVR = audioManagerZzL.getStreamVolume(3);
                this.zzVV = audioManagerZzL.getRingerMode();
                this.zzVW = audioManagerZzL.getStreamVolume(2);
                return;
            } catch (Throwable th) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "DeviceInfo.gatherAudioInfo");
            }
        }
        this.zzVF = -2;
        this.zzVM = false;
        this.zzVN = false;
        this.zzVR = 0;
        this.zzVV = 0;
        this.zzVW = 0;
    }

    @TargetApi(16)
    private final void zzl(Context context) {
        TelephonyManager telephonyManager = (TelephonyManager) context.getSystemService("phone");
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService("connectivity");
        this.zzVI = telephonyManager.getNetworkOperator();
        this.zzVT = telephonyManager.getNetworkType();
        this.zzVU = telephonyManager.getPhoneType();
        this.zzVS = -2;
        this.zzVZ = false;
        this.zzWa = -1;
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (zzagz.zzc(context, context.getPackageName(), "android.permission.ACCESS_NETWORK_STATE")) {
            NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
            if (activeNetworkInfo != null) {
                this.zzVS = activeNetworkInfo.getType();
                this.zzWa = activeNetworkInfo.getDetailedState().ordinal();
            } else {
                this.zzVS = -1;
            }
            if (Build.VERSION.SDK_INT >= 16) {
                this.zzVZ = connectivityManager.isActiveNetworkMetered();
            }
        }
    }

    private final void zzm(Context context) {
        Intent intentRegisterReceiver = context.registerReceiver(null, new IntentFilter("android.intent.action.BATTERY_CHANGED"));
        if (intentRegisterReceiver == null) {
            this.zzVX = -1.0d;
            this.zzVY = false;
        } else {
            int intExtra = intentRegisterReceiver.getIntExtra("status", -1);
            this.zzVX = intentRegisterReceiver.getIntExtra("level", -1) / intentRegisterReceiver.getIntExtra("scale", -1);
            this.zzVY = intExtra == 2 || intExtra == 5;
        }
    }

    public final zzacb zzgM() {
        return new zzacb(this.zzVF, this.zzVG, this.zzVH, this.zzVI, this.zzVJ, this.zzVK, this.zzVL, this.zzVM, this.zzVN, this.zzVO, this.zzVP, this.zzVQ, this.zzVR, this.zzVS, this.zzVT, this.zzVU, this.zzVV, this.zzVW, this.zzxR, this.zzSI, this.zzSJ, this.zzVX, this.zzVY, this.zzVZ, this.zzWa, this.zzWb, this.zzWd, this.zzWc);
    }
}
