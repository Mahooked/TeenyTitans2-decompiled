package com.google.android.gms.internal;

import android.content.Context;
import android.util.DisplayMetrics;
import android.view.MotionEvent;
import android.view.View;
import java.io.UnsupportedEncodingException;
import java.security.NoSuchAlgorithmException;
import java.util.Iterator;
import java.util.LinkedList;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzcq implements zzcp {
    protected MotionEvent zzpP;
    protected double zzpY;
    private double zzpZ;
    private double zzqa;
    protected float zzqb;
    protected float zzqc;
    protected float zzqd;
    protected float zzqe;
    protected DisplayMetrics zzqh;
    protected LinkedList<MotionEvent> zzpQ = new LinkedList<>();
    protected long zzpR = 0;
    protected long zzpS = 0;
    protected long zzpT = 0;
    protected long zzpU = 0;
    protected long zzpV = 0;
    protected long zzpW = 0;
    protected long zzpX = 0;
    private boolean zzqf = false;
    protected boolean zzqg = false;

    protected zzcq(Context context) {
        try {
            zzbv.zzw();
            this.zzqh = context.getResources().getDisplayMetrics();
        } catch (Throwable th) {
        }
    }

    private final String zza(Context context, String str, boolean z, View view, byte[] bArr) {
        zzau zzauVar;
        zzax zzaxVarZza;
        if (bArr == null || bArr.length <= 0) {
            zzauVar = null;
        } else {
            try {
                zzauVar = (zzau) adp.zza(new zzau(), bArr);
            } catch (ado e) {
                zzauVar = null;
            }
        }
        try {
            if (z) {
                zzax zzaxVarZza2 = zza(context, view);
                this.zzqf = true;
                zzaxVarZza = zzaxVarZza2;
            } else {
                zzaxVarZza = zza(context, zzauVar);
            }
            if (zzaxVarZza == null || zzaxVarZza.zzLV() == 0) {
                return Integer.toString(5);
            }
            return zzbv.zza(zzaxVarZza, str, !(!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEO)).booleanValue() || z));
        } catch (UnsupportedEncodingException e2) {
            return Integer.toString(7);
        } catch (NoSuchAlgorithmException e3) {
            return Integer.toString(7);
        } catch (Throwable th) {
            return Integer.toString(3);
        }
    }

    protected abstract long zza(StackTraceElement[] stackTraceElementArr) throws zzcy;

    protected abstract zzax zza(Context context, View view);

    protected abstract zzax zza(Context context, zzau zzauVar);

    @Override // com.google.android.gms.internal.zzcp
    public final String zza(Context context) {
        if (zzdg.zzS()) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFb)).booleanValue()) {
                throw new IllegalStateException("The caller must not be called from the UI thread.");
            }
        }
        return zza(context, null, false, null, null);
    }

    public final String zza(Context context, String str) {
        return zza(context, str, (View) null);
    }

    @Override // com.google.android.gms.internal.zzcp
    public final String zza(Context context, String str, View view) {
        return zza(context, str, true, view, null);
    }

    public final String zza(Context context, byte[] bArr) {
        if (zzdg.zzS()) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFb)).booleanValue()) {
                throw new IllegalStateException("The caller must not be called from the UI thread.");
            }
        }
        return zza(context, null, false, null, bArr);
    }

    @Override // com.google.android.gms.internal.zzcp
    public final void zza(int i, int i2, int i3) {
        if (this.zzpP != null) {
            this.zzpP.recycle();
        }
        if (this.zzqh != null) {
            this.zzpP = MotionEvent.obtain(0L, i3, 1, i * this.zzqh.density, i2 * this.zzqh.density, 0.0f, 0.0f, 0, 0.0f, 0.0f, 0, 0);
        } else {
            this.zzpP = null;
        }
        this.zzqg = false;
    }

    @Override // com.google.android.gms.internal.zzcp
    public final void zza(MotionEvent motionEvent) {
        if (this.zzqf) {
            this.zzpU = 0L;
            this.zzpT = 0L;
            this.zzpS = 0L;
            this.zzpR = 0L;
            this.zzpV = 0L;
            this.zzpX = 0L;
            this.zzpW = 0L;
            Iterator<MotionEvent> it = this.zzpQ.iterator();
            while (it.hasNext()) {
                it.next().recycle();
            }
            this.zzpQ.clear();
            this.zzpP = null;
            this.zzqf = false;
        }
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEU)).booleanValue()) {
            switch (motionEvent.getAction()) {
                case 0:
                    this.zzpY = 0.0d;
                    this.zzpZ = motionEvent.getRawX();
                    this.zzqa = motionEvent.getRawY();
                    break;
                case 1:
                case 2:
                    double rawX = motionEvent.getRawX();
                    double rawY = motionEvent.getRawY();
                    double d = rawX - this.zzpZ;
                    double d2 = rawY - this.zzqa;
                    this.zzpY = Math.sqrt((d * d) + (d2 * d2)) + this.zzpY;
                    this.zzpZ = rawX;
                    this.zzqa = rawY;
                    break;
            }
        }
        switch (motionEvent.getAction()) {
            case 0:
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEV)).booleanValue()) {
                    this.zzqb = motionEvent.getX();
                    this.zzqc = motionEvent.getY();
                    this.zzqd = motionEvent.getRawX();
                    this.zzqe = motionEvent.getRawY();
                }
                this.zzpR++;
                break;
            case 1:
                this.zzpP = MotionEvent.obtain(motionEvent);
                this.zzpQ.add(this.zzpP);
                if (this.zzpQ.size() > 6) {
                    this.zzpQ.remove().recycle();
                }
                this.zzpT++;
                try {
                    this.zzpV = zza(new Throwable().getStackTrace());
                } catch (zzcy e) {
                }
                break;
            case 2:
                this.zzpS += (long) (motionEvent.getHistorySize() + 1);
                try {
                    zzdf zzdfVarZzb = zzb(motionEvent);
                    if ((zzdfVarZzb == null || zzdfVarZzb.zzcd == null || zzdfVarZzb.zzrd == null) ? false : true) {
                        this.zzpW += zzdfVarZzb.zzcd.longValue() + zzdfVarZzb.zzrd.longValue();
                    }
                    if ((this.zzqh == null || zzdfVarZzb == null || zzdfVarZzb.zzcb == null || zzdfVarZzb.zzre == null) ? false : true) {
                        this.zzpX = zzdfVarZzb.zzre.longValue() + zzdfVarZzb.zzcb.longValue() + this.zzpX;
                    }
                } catch (zzcy e2) {
                }
                break;
            case 3:
                this.zzpU++;
                break;
        }
        this.zzqg = true;
    }

    protected abstract zzdf zzb(MotionEvent motionEvent) throws zzcy;
}
