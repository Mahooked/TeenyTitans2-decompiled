package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Bundle;
import com.comscore.streaming.Constants;
import com.google.android.gms.ads.AdActivity;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafl {
    private String mSessionId;
    private long zzYM = -1;
    private long zzYN = -1;
    private int zzYO = -1;
    int zzYI = -1;
    private long zzYP = 0;
    private final Object mLock = new Object();
    private int zzYQ = 0;
    private int zzYR = 0;

    public zzafl(String str) {
        this.mSessionId = str;
    }

    private static boolean zzB(Context context) {
        int identifier = context.getResources().getIdentifier("Theme.Translucent", "style", Constants.C10_VALUE);
        if (identifier == 0) {
            zzafr.zzaS("Please set theme of AdActivity to @android:style/Theme.Translucent to enable transparent background interstitial ad.");
            return false;
        }
        try {
            if (identifier == context.getPackageManager().getActivityInfo(new ComponentName(context.getPackageName(), AdActivity.CLASS_NAME), 0).theme) {
                return true;
            }
            zzafr.zzaS("Please set theme of AdActivity to @android:style/Theme.Translucent to enable transparent background interstitial ad.");
            return false;
        } catch (PackageManager.NameNotFoundException e) {
            zzafr.zzaT("Fail to fetch AdActivity theme");
            zzafr.zzaS("Please set theme of AdActivity to @android:style/Theme.Translucent to enable transparent background interstitial ad.");
            return false;
        }
    }

    public final void zzb(zzir zzirVar, long j) {
        synchronized (this.mLock) {
            long jZzhy = com.google.android.gms.ads.internal.zzbs.zzbD().zzhy();
            long jCurrentTimeMillis = com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis();
            if (this.zzYN == -1) {
                if (jCurrentTimeMillis - jZzhy > ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDL)).longValue()) {
                    this.zzYI = -1;
                } else {
                    this.zzYI = com.google.android.gms.ads.internal.zzbs.zzbD().zzhB();
                }
                this.zzYN = j;
                this.zzYM = this.zzYN;
            } else {
                this.zzYM = j;
            }
            if (zzirVar.extras == null || zzirVar.extras.getInt("gw", 2) != 1) {
                this.zzYO++;
                this.zzYI++;
                if (this.zzYI == 0) {
                    this.zzYP = 0L;
                    com.google.android.gms.ads.internal.zzbs.zzbD().zzk(jCurrentTimeMillis);
                } else {
                    this.zzYP = jCurrentTimeMillis - com.google.android.gms.ads.internal.zzbs.zzbD().zzhz();
                }
            }
        }
    }

    public final void zzhc() {
        synchronized (this.mLock) {
            this.zzYR++;
        }
    }

    public final void zzhd() {
        synchronized (this.mLock) {
            this.zzYQ++;
        }
    }

    public final Bundle zzo(Context context, String str) {
        Bundle bundle;
        synchronized (this.mLock) {
            bundle = new Bundle();
            bundle.putString("session_id", this.mSessionId);
            bundle.putLong("basets", this.zzYN);
            bundle.putLong("currts", this.zzYM);
            bundle.putString("seq_num", str);
            bundle.putInt("preqs", this.zzYO);
            bundle.putInt("preqs_in_session", this.zzYI);
            bundle.putLong("time_in_session", this.zzYP);
            bundle.putInt("pclick", this.zzYQ);
            bundle.putInt("pimp", this.zzYR);
            bundle.putBoolean("support_transparent_background", zzB(context));
        }
        return bundle;
    }
}
