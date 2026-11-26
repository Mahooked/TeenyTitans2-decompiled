package com.google.android.gms.internal;

import android.content.Context;
import android.os.Parcel;
import android.os.Parcelable;
import android.util.DisplayMetrics;
import com.google.android.gms.ads.AdSize;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zziv extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zziv> CREATOR = new zziw();
    public final int height;
    public final int heightPixels;
    public final int width;
    public final int widthPixels;
    public final String zzAs;
    public final boolean zzAt;
    public final zziv[] zzAu;
    public final boolean zzAv;
    public final boolean zzAw;
    public boolean zzAx;

    public zziv() {
        this("interstitial_mb", 0, 0, true, 0, 0, null, false, false, false);
    }

    public zziv(Context context, AdSize adSize) {
        this(context, new AdSize[]{adSize});
    }

    /* JADX WARN: Code duplicated, block: B:37:0x00d0  */
    public zziv(Context context, AdSize[] adSizeArr) {
        int i;
        AdSize adSize = adSizeArr[0];
        this.zzAt = false;
        this.zzAw = adSize.isFluid();
        if (this.zzAw) {
            this.width = AdSize.BANNER.getWidth();
            this.height = AdSize.BANNER.getHeight();
        } else {
            this.width = adSize.getWidth();
            this.height = adSize.getHeight();
        }
        boolean z = this.width == -1;
        boolean z2 = this.height == -2;
        DisplayMetrics displayMetrics = context.getResources().getDisplayMetrics();
        if (z) {
            zzji.zzds();
            if (zzaiy.zzZ(context)) {
                zzji.zzds();
                if (zzaiy.zzaa(context)) {
                    int i2 = displayMetrics.widthPixels;
                    zzji.zzds();
                    this.widthPixels = i2 - zzaiy.zzab(context);
                } else {
                    this.widthPixels = displayMetrics.widthPixels;
                }
            } else {
                this.widthPixels = displayMetrics.widthPixels;
            }
            double d = this.widthPixels / displayMetrics.density;
            int i3 = (int) d;
            i = d - ((double) ((int) d)) >= 0.01d ? i3 + 1 : i3;
        } else {
            int i4 = this.width;
            zzji.zzds();
            this.widthPixels = zzaiy.zza(displayMetrics, this.width);
            i = i4;
        }
        int iZzc = z2 ? zzc(displayMetrics) : this.height;
        zzji.zzds();
        this.heightPixels = zzaiy.zza(displayMetrics, iZzc);
        if (z || z2) {
            this.zzAs = new StringBuilder(26).append(i).append("x").append(iZzc).append("_as").toString();
        } else if (this.zzAw) {
            this.zzAs = "320x50_mb";
        } else {
            this.zzAs = adSize.toString();
        }
        if (adSizeArr.length > 1) {
            this.zzAu = new zziv[adSizeArr.length];
            for (int i5 = 0; i5 < adSizeArr.length; i5++) {
                this.zzAu[i5] = new zziv(context, adSizeArr[i5]);
            }
        } else {
            this.zzAu = null;
        }
        this.zzAv = false;
        this.zzAx = false;
    }

    public zziv(zziv zzivVar, zziv[] zzivVarArr) {
        this(zzivVar.zzAs, zzivVar.height, zzivVar.heightPixels, zzivVar.zzAt, zzivVar.width, zzivVar.widthPixels, zzivVarArr, zzivVar.zzAv, zzivVar.zzAw, zzivVar.zzAx);
    }

    zziv(String str, int i, int i2, boolean z, int i3, int i4, zziv[] zzivVarArr, boolean z2, boolean z3, boolean z4) {
        this.zzAs = str;
        this.height = i;
        this.heightPixels = i2;
        this.zzAt = z;
        this.width = i3;
        this.widthPixels = i4;
        this.zzAu = zzivVarArr;
        this.zzAv = z2;
        this.zzAw = z3;
        this.zzAx = z4;
    }

    public static int zza(DisplayMetrics displayMetrics) {
        return displayMetrics.widthPixels;
    }

    public static int zzb(DisplayMetrics displayMetrics) {
        return (int) (zzc(displayMetrics) * displayMetrics.density);
    }

    private static int zzc(DisplayMetrics displayMetrics) {
        int i = (int) (displayMetrics.heightPixels / displayMetrics.density);
        if (i <= 400) {
            return 32;
        }
        return i <= 720 ? 50 : 90;
    }

    public static zziv zzdk() {
        return new zziv("reward_mb", 0, 0, true, 0, 0, null, false, false, false);
    }

    public static zziv zzg(Context context) {
        return new zziv("320x50_mb", 0, 0, false, 0, 0, null, true, false, false);
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzAs, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.height);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 4, this.heightPixels);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzAt);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 6, this.width);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 7, this.widthPixels);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, (Parcelable[]) this.zzAu, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzAv);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 10, this.zzAw);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 11, this.zzAx);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final AdSize zzdl() {
        return com.google.android.gms.ads.zzb.zza(this.width, this.height, this.zzAs);
    }
}
