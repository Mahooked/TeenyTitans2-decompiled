package com.google.android.gms.ads.internal.overlay;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaia;
import com.google.android.gms.internal.zzaic;
import com.google.android.gms.internal.zzaid;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzmu;
import com.google.android.gms.internal.zzmz;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zzzn;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzar {
    private final Context mContext;

    @Nullable
    private final zznb zzPs;
    private boolean zzPw;
    private final long[] zzQA;
    private final String[] zzQB;
    private zzy zzQG;
    private boolean zzQH;
    private boolean zzQI;
    private final String zzQx;

    @Nullable
    private final zzmz zzQy;
    private final zzaje zzuK;
    private final zzaia zzQz = new zzaid().zza("min_1", Double.MIN_VALUE, 1.0d).zza("1_5", 1.0d, 5.0d).zza("5_10", 5.0d, 10.0d).zza("10_20", 10.0d, 20.0d).zza("20_30", 20.0d, 30.0d).zza("30_max", 30.0d, Double.MAX_VALUE).zzid();
    private boolean zzQC = false;
    private boolean zzQD = false;
    private boolean zzQE = false;
    private boolean zzQF = false;
    private long zzQJ = -1;

    public zzar(Context context, zzaje zzajeVar, String str, @Nullable zznb zznbVar, @Nullable zzmz zzmzVar) {
        this.mContext = context;
        this.zzuK = zzajeVar;
        this.zzQx = str;
        this.zzPs = zznbVar;
        this.zzQy = zzmzVar;
        String str2 = (String) zzbs.zzbL().zzd(zzmo.zzCu);
        if (str2 == null) {
            this.zzQB = new String[0];
            this.zzQA = new long[0];
            return;
        }
        String[] strArrSplit = TextUtils.split(str2, ",");
        this.zzQB = new String[strArrSplit.length];
        this.zzQA = new long[strArrSplit.length];
        for (int i = 0; i < strArrSplit.length; i++) {
            try {
                this.zzQA[i] = Long.parseLong(strArrSplit[i]);
            } catch (NumberFormatException e) {
                zzafr.zzc("Unable to parse frame hash target time number.", e);
                this.zzQA[i] = -1;
            }
        }
    }

    public void onStop() {
        if (!((Boolean) zzbs.zzbL().zzd(zzmo.zzCt)).booleanValue() || this.zzQH) {
            return;
        }
        Bundle bundle = new Bundle();
        bundle.putString("type", "native-player-metrics");
        bundle.putString("request", this.zzQx);
        bundle.putString("player", this.zzQG.zzfD());
        for (zzaic zzaicVar : this.zzQz.getBuckets()) {
            String strValueOf = String.valueOf("fps_c_");
            String strValueOf2 = String.valueOf(zzaicVar.name);
            bundle.putString(strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf), Integer.toString(zzaicVar.count));
            String strValueOf3 = String.valueOf("fps_p_");
            String strValueOf4 = String.valueOf(zzaicVar.name);
            bundle.putString(strValueOf4.length() != 0 ? strValueOf3.concat(strValueOf4) : new String(strValueOf3), Double.toString(zzaicVar.zzZZ));
        }
        for (int i = 0; i < this.zzQA.length; i++) {
            String str = this.zzQB[i];
            if (str != null) {
                String strValueOf5 = String.valueOf("fh_");
                String strValueOf6 = String.valueOf(Long.valueOf(this.zzQA[i]));
                bundle.putString(new StringBuilder(String.valueOf(strValueOf5).length() + String.valueOf(strValueOf6).length()).append(strValueOf5).append(strValueOf6).toString(), str);
            }
        }
        zzbs.zzbz().zza(this.mContext, this.zzuK.zzaP, "gmob-apps", bundle, true);
        this.zzQH = true;
    }

    public final void zza(zzy zzyVar) {
        zzmu.zza(this.zzPs, this.zzQy, "vpc2");
        this.zzQC = true;
        if (this.zzPs != null) {
            this.zzPs.zzh("vpn", zzyVar.zzfD());
        }
        this.zzQG = zzyVar;
    }

    public final void zzb(zzy zzyVar) {
        long j;
        if (this.zzQE && !this.zzQF) {
            zzmu.zza(this.zzPs, this.zzQy, "vff2");
            this.zzQF = true;
        }
        long jNanoTime = zzbs.zzbF().nanoTime();
        if (this.zzPw && this.zzQI && this.zzQJ != -1) {
            this.zzQz.zza(TimeUnit.SECONDS.toNanos(1L) / (jNanoTime - this.zzQJ));
        }
        this.zzQI = this.zzPw;
        this.zzQJ = jNanoTime;
        long jLongValue = ((Long) zzbs.zzbL().zzd(zzmo.zzCv)).longValue();
        long currentPosition = zzyVar.getCurrentPosition();
        for (int i = 0; i < this.zzQB.length; i++) {
            if (this.zzQB[i] == null && jLongValue > Math.abs(currentPosition - this.zzQA[i])) {
                String[] strArr = this.zzQB;
                Bitmap bitmap = zzyVar.getBitmap(8, 8);
                long j2 = 0;
                long j3 = 63;
                int i2 = 0;
                while (i2 < 8) {
                    int i3 = 0;
                    long j4 = j2;
                    while (true) {
                        j = j3;
                        if (i3 < 8) {
                            int pixel = bitmap.getPixel(i3, i2);
                            j4 |= (Color.green(pixel) + (Color.blue(pixel) + Color.red(pixel)) > 128 ? 1L : 0L) << ((int) j);
                            i3++;
                            j3 = j - 1;
                        }
                    }
                    i2++;
                    j3 = j;
                    j2 = j4;
                }
                strArr[i] = String.format("%016X", Long.valueOf(j2));
                return;
            }
        }
    }

    public final void zzfT() {
        if (!this.zzQC || this.zzQD) {
            return;
        }
        zzmu.zza(this.zzPs, this.zzQy, "vfr2");
        this.zzQD = true;
    }

    public final void zzgj() {
        this.zzPw = true;
        if (!this.zzQD || this.zzQE) {
            return;
        }
        zzmu.zza(this.zzPs, this.zzQy, "vfp2");
        this.zzQE = true;
    }

    public final void zzgk() {
        this.zzPw = false;
    }
}
