package com.google.android.gms.ads.internal.overlay;

import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.text.TextUtils;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zza {
    private static boolean zza(Context context, Intent intent, zzag zzagVar) {
        try {
            String strValueOf = String.valueOf(intent.toURI());
            zzafr.v(strValueOf.length() != 0 ? "Launching an intent: ".concat(strValueOf) : new String("Launching an intent: "));
            zzbs.zzbz();
            zzagz.zzb(context, intent);
            if (zzagVar != null) {
                zzagVar.zzan();
            }
            return true;
        } catch (ActivityNotFoundException e) {
            zzafr.zzaT(e.getMessage());
            return false;
        }
    }

    public static boolean zza(Context context, zzc zzcVar, zzag zzagVar) {
        int i;
        if (zzcVar == null) {
            zzafr.zzaT("No intent data for launcher overlay.");
            return false;
        }
        zzmo.initialize(context);
        if (zzcVar.intent != null) {
            return zza(context, zzcVar.intent, zzagVar);
        }
        Intent intent = new Intent();
        if (TextUtils.isEmpty(zzcVar.url)) {
            zzafr.zzaT("Open GMSG did not contain a URL.");
            return false;
        }
        if (TextUtils.isEmpty(zzcVar.mimeType)) {
            intent.setData(Uri.parse(zzcVar.url));
        } else {
            intent.setDataAndType(Uri.parse(zzcVar.url), zzcVar.mimeType);
        }
        intent.setAction("android.intent.action.VIEW");
        if (!TextUtils.isEmpty(zzcVar.packageName)) {
            intent.setPackage(zzcVar.packageName);
        }
        if (!TextUtils.isEmpty(zzcVar.zzOh)) {
            String[] strArrSplit = zzcVar.zzOh.split("/", 2);
            if (strArrSplit.length < 2) {
                String strValueOf = String.valueOf(zzcVar.zzOh);
                zzafr.zzaT(strValueOf.length() != 0 ? "Could not parse component name from open GMSG: ".concat(strValueOf) : new String("Could not parse component name from open GMSG: "));
                return false;
            }
            intent.setClassName(strArrSplit[0], strArrSplit[1]);
        }
        String str = zzcVar.zzOi;
        if (!TextUtils.isEmpty(str)) {
            try {
                i = Integer.parseInt(str);
            } catch (NumberFormatException e) {
                zzafr.zzaT("Could not parse intent flags.");
                i = 0;
            }
            intent.addFlags(i);
        }
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGq)).booleanValue()) {
            intent.addFlags(268435456);
            intent.putExtra("android.support.customtabs.extra.user_opt_out", true);
        } else {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGp)).booleanValue()) {
                zzbs.zzbz();
                zzagz.zzc(context, intent);
            }
        }
        return zza(context, intent, zzagVar);
    }
}
