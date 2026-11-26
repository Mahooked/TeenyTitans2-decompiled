package com.google.android.gms.internal;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.PowerManager;
import android.os.WorkSource;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
public final class zzctz {
    private final Context mContext;
    private final String zzaJp;
    private final String zzaJr;
    private final PowerManager.WakeLock zzbCX;
    private WorkSource zzbCY;
    private final int zzbCZ;
    private final String zzbDa;
    private boolean zzbDb;
    private int zzbDc;
    private int zzbDd;
    private static String TAG = "WakeLock";
    private static String zzbCW = "*gcore*:";
    private static boolean DEBUG = false;

    public zzctz(Context context, int i, String str) {
        this(context, 1, str, null, context == null ? null : context.getPackageName());
    }

    @SuppressLint({"UnwrappedWakeLock"})
    private zzctz(Context context, int i, String str, String str2, String str3) {
        this(context, 1, str, null, str3, null);
    }

    @SuppressLint({"UnwrappedWakeLock"})
    private zzctz(Context context, int i, String str, String str2, String str3, String str4) {
        this.zzbDb = true;
        zzbo.zzh(str, "Wake lock name can NOT be empty");
        this.zzbCZ = i;
        this.zzbDa = null;
        this.zzaJr = null;
        this.mContext = context.getApplicationContext();
        if ("com.google.android.gms".equals(context.getPackageName())) {
            this.zzaJp = str;
        } else {
            String strValueOf = String.valueOf(zzbCW);
            String strValueOf2 = String.valueOf(str);
            this.zzaJp = strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf);
        }
        this.zzbCX = ((PowerManager) context.getSystemService("power")).newWakeLock(i, str);
        if (com.google.android.gms.common.util.zzx.zzaM(this.mContext)) {
            this.zzbCY = com.google.android.gms.common.util.zzx.zzD(context, com.google.android.gms.common.util.zzt.zzcL(str3) ? context.getPackageName() : str3);
            WorkSource workSource = this.zzbCY;
            if (workSource == null || !com.google.android.gms.common.util.zzx.zzaM(this.mContext)) {
                return;
            }
            if (this.zzbCY != null) {
                this.zzbCY.add(workSource);
            } else {
                this.zzbCY = workSource;
            }
            try {
                this.zzbCX.setWorkSource(this.zzbCY);
            } catch (IllegalArgumentException e) {
                Log.wtf(TAG, e.toString());
            }
        }
    }

    private final boolean zzeV(String str) {
        String str2 = null;
        return (TextUtils.isEmpty(null) || str2.equals(this.zzbDa)) ? false : true;
    }

    private final String zzi(String str, boolean z) {
        if (this.zzbDb && z) {
            return null;
        }
        return this.zzbDa;
    }

    /* JADX WARN: Code duplicated, block: B:13:0x0022 A[Catch: all -> 0x004b, TryCatch #0 {, blocks: (B:4:0x000c, B:6:0x0010, B:13:0x0022, B:14:0x0044, B:9:0x001a, B:11:0x001e), top: B:20:0x000c }] */
    /* JADX WARN: Code duplicated, block: B:9:0x001a A[Catch: all -> 0x004b, TryCatch #0 {, blocks: (B:4:0x000c, B:6:0x0010, B:13:0x0022, B:14:0x0044, B:9:0x001a, B:11:0x001e), top: B:20:0x000c }] */
    public final void acquire(long j) {
        boolean zZzeV = zzeV(null);
        String strZzi = zzi(null, zZzeV);
        synchronized (this) {
            if (this.zzbDb) {
                int i = this.zzbDc;
                this.zzbDc = i + 1;
                if (i == 0 || zZzeV) {
                    com.google.android.gms.common.stats.zze.zzrX();
                    com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 7, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY), 1000L);
                    this.zzbDd++;
                } else if (!this.zzbDb && this.zzbDd == 0) {
                    com.google.android.gms.common.stats.zze.zzrX();
                    com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 7, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY), 1000L);
                    this.zzbDd++;
                }
            } else if (!this.zzbDb) {
                com.google.android.gms.common.stats.zze.zzrX();
                com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 7, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY), 1000L);
                this.zzbDd++;
            }
        }
        this.zzbCX.acquire(1000L);
    }

    public final boolean isHeld() {
        return this.zzbCX.isHeld();
    }

    /* JADX WARN: Code duplicated, block: B:13:0x0021 A[Catch: all -> 0x0049, TryCatch #0 {, blocks: (B:4:0x000a, B:6:0x000e, B:13:0x0021, B:14:0x0042, B:9:0x0018, B:11:0x001c), top: B:20:0x000a }] */
    /* JADX WARN: Code duplicated, block: B:9:0x0018 A[Catch: all -> 0x0049, TryCatch #0 {, blocks: (B:4:0x000a, B:6:0x000e, B:13:0x0021, B:14:0x0042, B:9:0x0018, B:11:0x001c), top: B:20:0x000a }] */
    public final void release() {
        boolean zZzeV = zzeV(null);
        String strZzi = zzi(null, zZzeV);
        synchronized (this) {
            if (this.zzbDb) {
                int i = this.zzbDc - 1;
                this.zzbDc = i;
                if (i == 0 || zZzeV) {
                    com.google.android.gms.common.stats.zze.zzrX();
                    com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 8, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY));
                    this.zzbDd--;
                } else if (!this.zzbDb && this.zzbDd == 1) {
                    com.google.android.gms.common.stats.zze.zzrX();
                    com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 8, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY));
                    this.zzbDd--;
                }
            } else if (!this.zzbDb) {
                com.google.android.gms.common.stats.zze.zzrX();
                com.google.android.gms.common.stats.zze.zza(this.mContext, com.google.android.gms.common.stats.zzc.zza(this.zzbCX, strZzi), 8, this.zzaJp, strZzi, null, this.zzbCZ, com.google.android.gms.common.util.zzx.zzb(this.zzbCY));
                this.zzbDd--;
            }
        }
        this.zzbCX.release();
    }

    public final void setReferenceCounted(boolean z) {
        this.zzbCX.setReferenceCounted(false);
        this.zzbDb = false;
    }
}
