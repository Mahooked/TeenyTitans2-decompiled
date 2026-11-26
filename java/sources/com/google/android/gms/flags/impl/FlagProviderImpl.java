package com.google.android.gms.flags.impl;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.util.Log;
import com.google.android.gms.common.util.DynamiteApi;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.internal.zzcad;

/* JADX INFO: loaded from: classes.dex */
@DynamiteApi
public class FlagProviderImpl extends zzcad {
    private SharedPreferences zzBT;
    private boolean zzuH = false;

    @Override // com.google.android.gms.internal.zzcac
    public boolean getBooleanFlagValue(String str, boolean z, int i) {
        return !this.zzuH ? z : zzb.zza(this.zzBT, str, Boolean.valueOf(z)).booleanValue();
    }

    @Override // com.google.android.gms.internal.zzcac
    public int getIntFlagValue(String str, int i, int i2) {
        return !this.zzuH ? i : zzd.zza(this.zzBT, str, Integer.valueOf(i)).intValue();
    }

    @Override // com.google.android.gms.internal.zzcac
    public long getLongFlagValue(String str, long j, int i) {
        return !this.zzuH ? j : zzf.zza(this.zzBT, str, Long.valueOf(j)).longValue();
    }

    @Override // com.google.android.gms.internal.zzcac
    public String getStringFlagValue(String str, String str2, int i) {
        return !this.zzuH ? str2 : zzh.zza(this.zzBT, str, str2);
    }

    @Override // com.google.android.gms.internal.zzcac
    public void init(IObjectWrapper iObjectWrapper) {
        Context context = (Context) zzn.zzE(iObjectWrapper);
        if (this.zzuH) {
            return;
        }
        try {
            this.zzBT = zzj.zzaW(context.createPackageContext("com.google.android.gms", 0));
            this.zzuH = true;
        } catch (PackageManager.NameNotFoundException e) {
        } catch (Exception e2) {
            String strValueOf = String.valueOf(e2.getMessage());
            Log.w("FlagProviderImpl", strValueOf.length() != 0 ? "Could not retrieve sdk flags, continuing with defaults: ".concat(strValueOf) : new String("Could not retrieve sdk flags, continuing with defaults: "));
        }
    }
}
