package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;
import android.text.TextUtils;
import java.util.HashMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import org.apache.http.protocol.HTTP;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzahw {
    private final Object mLock = new Object();
    private String zzZK = "";
    private String zzZL = "";
    private boolean zzZM = false;
    private String zzZN = "";

    private final String zzT(Context context) {
        String str;
        synchronized (this.mLock) {
            if (TextUtils.isEmpty(this.zzZK)) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                this.zzZK = zzagz.zzt(context, "debug_signals_id.txt");
                if (TextUtils.isEmpty(this.zzZK)) {
                    com.google.android.gms.ads.internal.zzbs.zzbz();
                    this.zzZK = zzagz.zzhO();
                    com.google.android.gms.ads.internal.zzbs.zzbz();
                    zzagz.zze(context, "debug_signals_id.txt", this.zzZK);
                }
            }
            str = this.zzZK;
        }
        return str;
    }

    private final void zza(Context context, String str, boolean z, boolean z2) {
        if (context instanceof Activity) {
            zzagz.zzZr.post(new zzahx(this, context, str, z, z2));
        } else {
            zzafr.zzaS("Can not create dialog without Activity Context");
        }
    }

    private final Uri zzb(Context context, String str, String str2, String str3) {
        Uri.Builder builderBuildUpon = Uri.parse(str).buildUpon();
        builderBuildUpon.appendQueryParameter("linkedDeviceId", zzT(context));
        builderBuildUpon.appendQueryParameter("adSlotPath", str2);
        builderBuildUpon.appendQueryParameter("afmaVersion", str3);
        return builderBuildUpon.build();
    }

    private final boolean zzh(Context context, String str, String str2) {
        String strZzj = zzj(context, zzb(context, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGu), str, str2).toString(), str2);
        if (TextUtils.isEmpty(strZzj)) {
            zzafr.zzaC("Not linked for in app preview.");
            return false;
        }
        try {
            JSONObject jSONObject = new JSONObject(strZzj.trim());
            String strOptString = jSONObject.optString("gct");
            this.zzZN = jSONObject.optString("status");
            synchronized (this.mLock) {
                this.zzZL = strOptString;
            }
            return true;
        } catch (JSONException e) {
            zzafr.zzc("Fail to get in app preview response json.", e);
            return false;
        }
    }

    private final boolean zzi(Context context, String str, String str2) {
        String strZzj = zzj(context, zzb(context, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGv), str, str2).toString(), str2);
        if (TextUtils.isEmpty(strZzj)) {
            zzafr.zzaC("Not linked for debug signals.");
            return false;
        }
        try {
            boolean zEquals = "1".equals(new JSONObject(strZzj.trim()).optString("debug_mode"));
            synchronized (this.mLock) {
                this.zzZM = zEquals;
            }
            return zEquals;
        } catch (JSONException e) {
            zzafr.zzc("Fail to get debug mode response json.", e);
            return false;
        }
    }

    private static String zzj(Context context, String str, String str2) {
        HashMap map = new HashMap();
        map.put(HTTP.USER_AGENT, com.google.android.gms.ads.internal.zzbs.zzbz().zzs(context, str2));
        zzajm<String> zzajmVarZzb = new zzaie(context).zzb(str, map);
        try {
            return zzajmVarZzb.get(((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGx)).intValue(), TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            String strValueOf = String.valueOf(str);
            zzafr.zzb(strValueOf.length() != 0 ? "Interrupted while retriving a response from: ".concat(strValueOf) : new String("Interrupted while retriving a response from: "), e);
            zzajmVarZzb.cancel(true);
            return null;
        } catch (TimeoutException e2) {
            String strValueOf2 = String.valueOf(str);
            zzafr.zzb(strValueOf2.length() != 0 ? "Timeout while retriving a response from: ".concat(strValueOf2) : new String("Timeout while retriving a response from: "), e2);
            zzajmVarZzb.cancel(true);
            return null;
        } catch (Exception e3) {
            String strValueOf3 = String.valueOf(str);
            zzafr.zzb(strValueOf3.length() != 0 ? "Error retriving a response from: ".concat(strValueOf3) : new String("Error retriving a response from: "), e3);
            return null;
        }
    }

    private final void zzk(Context context, String str, String str2) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zza(context, zzb(context, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGt), str, str2));
    }

    public final void zza(Context context, String str, String str2, String str3) {
        Uri.Builder builderBuildUpon = zzb(context, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGw), str3, str).buildUpon();
        builderBuildUpon.appendQueryParameter("debugData", str2);
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zzd(context, str, builderBuildUpon.build().toString());
    }

    public final void zzf(Context context, String str, String str2) {
        if (!zzh(context, str, str2)) {
            zza(context, "In-app preview failed to load because of a system error. Please try again later.", true, true);
            return;
        }
        if ("2".equals(this.zzZN)) {
            zzafr.zzaC("Creative is not pushed for this device.");
            zza(context, "There was no creative pushed from DFP to the device.", false, false);
        } else if ("1".equals(this.zzZN)) {
            zzafr.zzaC("The app is not linked for creative preview.");
            zzk(context, str, str2);
        } else if ("0".equals(this.zzZN)) {
            zzafr.zzaC("Device is linked for in app preview.");
            zza(context, "The device is successfully linked for creative preview.", false, true);
        }
    }

    public final void zzg(Context context, String str, String str2) {
        if (!zzi(context, str, str2)) {
            zzk(context, str, str2);
        } else {
            zzafr.zzaC("Device is linked for debug signals.");
            zza(context, "The device is successfully linked for troubleshooting.", false, true);
        }
    }

    public final String zzib() {
        String str;
        synchronized (this.mLock) {
            str = this.zzZL;
        }
        return str;
    }

    public final boolean zzic() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzZM;
        }
        return z;
    }
}
