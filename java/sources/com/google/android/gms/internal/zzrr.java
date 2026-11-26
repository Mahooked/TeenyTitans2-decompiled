package com.google.android.gms.internal;

import android.app.ActivityManager;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.apache.http.HttpHost;

/* JADX INFO: loaded from: classes.dex */
public final class zzrr {
    private final zzaka zzJH;

    public zzrr(zzaka zzakaVar) {
        this.zzJH = zzakaVar;
    }

    private static Intent zza(Intent intent, ResolveInfo resolveInfo) {
        Intent intent2 = new Intent(intent);
        intent2.setClassName(resolveInfo.activityInfo.packageName, resolveInfo.activityInfo.name);
        return intent2;
    }

    private static ResolveInfo zza(Context context, Intent intent) {
        return zza(context, intent, new ArrayList());
    }

    private static ResolveInfo zza(Context context, Intent intent, ArrayList<ResolveInfo> arrayList) {
        ResolveInfo resolveInfo;
        PackageManager packageManager = context.getPackageManager();
        if (packageManager == null) {
            return null;
        }
        List<ResolveInfo> listQueryIntentActivities = packageManager.queryIntentActivities(intent, 65536);
        ResolveInfo resolveInfoResolveActivity = packageManager.resolveActivity(intent, 65536);
        if (listQueryIntentActivities == null || resolveInfoResolveActivity == null) {
            resolveInfo = null;
        } else {
            int i = 0;
            while (true) {
                int i2 = i;
                if (i2 >= listQueryIntentActivities.size()) {
                    break;
                }
                ResolveInfo resolveInfo2 = listQueryIntentActivities.get(i2);
                if (resolveInfoResolveActivity == null || !resolveInfoResolveActivity.activityInfo.name.equals(resolveInfo2.activityInfo.name)) {
                    i = i2 + 1;
                } else {
                    resolveInfo = resolveInfoResolveActivity;
                }
            }
            resolveInfo = null;
        }
        arrayList.addAll(listQueryIntentActivities);
        return resolveInfo;
    }

    private static Intent zzf(Uri uri) {
        if (uri == null) {
            return null;
        }
        Intent intent = new Intent("android.intent.action.VIEW");
        intent.addFlags(268435456);
        intent.setData(uri);
        intent.setAction("android.intent.action.VIEW");
        return intent;
    }

    /* JADX WARN: Code duplicated, block: B:12:0x0062  */
    public final Intent zza(Context context, Map<String, String> map) {
        boolean z;
        List<ActivityManager.RunningAppProcessInfo> runningAppProcesses;
        ResolveInfo resolveInfoZza;
        Uri uriBuild = null;
        ActivityManager activityManager = (ActivityManager) context.getSystemService("activity");
        String strZzb = map.get("u");
        if (TextUtils.isEmpty(strZzb)) {
            return null;
        }
        if (this.zzJH != null) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            strZzb = zzagz.zzb(this.zzJH, strZzb);
        }
        Uri uri = Uri.parse(strZzb);
        boolean z2 = Boolean.parseBoolean(map.get("use_first_package"));
        boolean z3 = Boolean.parseBoolean(map.get("use_running_process"));
        if (Boolean.parseBoolean(map.get("use_custom_tabs"))) {
            z = true;
        } else if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGp)).booleanValue()) {
            z = true;
        } else {
            z = false;
        }
        if (HttpHost.DEFAULT_SCHEME_NAME.equalsIgnoreCase(uri.getScheme())) {
            uriBuild = uri.buildUpon().scheme("https").build();
        } else if ("https".equalsIgnoreCase(uri.getScheme())) {
            uriBuild = uri.buildUpon().scheme(HttpHost.DEFAULT_SCHEME_NAME).build();
        }
        ArrayList arrayList = new ArrayList();
        Intent intentZzf = zzf(uri);
        Intent intentZzf2 = zzf(uriBuild);
        if (z) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            zzagz.zzc(context, intentZzf);
            com.google.android.gms.ads.internal.zzbs.zzbz();
            zzagz.zzc(context, intentZzf2);
        }
        ResolveInfo resolveInfoZza2 = zza(context, intentZzf, arrayList);
        if (resolveInfoZza2 != null) {
            return zza(intentZzf, resolveInfoZza2);
        }
        if (intentZzf2 != null && (resolveInfoZza = zza(context, intentZzf2)) != null) {
            Intent intentZza = zza(intentZzf, resolveInfoZza);
            if (zza(context, intentZza) != null) {
                return intentZza;
            }
        }
        if (arrayList.size() == 0) {
            return intentZzf;
        }
        if (z3 && activityManager != null && (runningAppProcesses = activityManager.getRunningAppProcesses()) != null) {
            ArrayList arrayList2 = arrayList;
            int size = arrayList2.size();
            int i = 0;
            while (i < size) {
                int i2 = i + 1;
                ResolveInfo resolveInfo = (ResolveInfo) arrayList2.get(i);
                Iterator<ActivityManager.RunningAppProcessInfo> it = runningAppProcesses.iterator();
                while (it.hasNext()) {
                    if (it.next().processName.equals(resolveInfo.activityInfo.packageName)) {
                        return zza(intentZzf, resolveInfo);
                    }
                }
                i = i2;
            }
        }
        return z2 ? zza(intentZzf, (ResolveInfo) arrayList.get(0)) : intentZzf;
    }
}
