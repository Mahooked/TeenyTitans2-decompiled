package com.google.android.gms.internal;

import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.v4.view.MotionEventCompat;
import com.comscore.utils.Constants;
import com.google.ads.AdSize;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zzaag implements Parcelable.Creator<zzaae> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaae createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        Bundle bundleZzs = null;
        zzir zzirVar = null;
        zziv zzivVar = null;
        String strZzq = null;
        ApplicationInfo applicationInfo = null;
        PackageInfo packageInfo = null;
        String strZzq2 = null;
        String strZzq3 = null;
        String strZzq4 = null;
        zzaje zzajeVar = null;
        Bundle bundleZzs2 = null;
        int iZzg2 = 0;
        ArrayList<String> arrayListZzC = null;
        Bundle bundleZzs3 = null;
        boolean zZzc = false;
        int iZzg3 = 0;
        int iZzg4 = 0;
        float fZzl = 0.0f;
        String strZzq5 = null;
        long jZzi = 0;
        String strZzq6 = null;
        ArrayList<String> arrayListZzC2 = null;
        String strZzq7 = null;
        zzon zzonVar = null;
        ArrayList<String> arrayListZzC3 = null;
        long jZzi2 = 0;
        String strZzq8 = null;
        float fZzl2 = 0.0f;
        boolean zZzc2 = false;
        int iZzg5 = 0;
        int iZzg6 = 0;
        boolean zZzc3 = false;
        boolean zZzc4 = false;
        String strZzq9 = null;
        String strZzq10 = null;
        boolean zZzc5 = false;
        int iZzg7 = 0;
        Bundle bundleZzs4 = null;
        String strZzq11 = null;
        zzky zzkyVar = null;
        boolean zZzc6 = false;
        Bundle bundleZzs5 = null;
        String strZzq12 = null;
        String strZzq13 = null;
        String strZzq14 = null;
        boolean zZzc7 = false;
        ArrayList<Integer> arrayListZzB = null;
        String strZzq15 = null;
        ArrayList<String> arrayListZzC4 = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    bundleZzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 3:
                    zzirVar = (zzir) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzir.CREATOR);
                    break;
                case 4:
                    zzivVar = (zziv) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zziv.CREATOR);
                    break;
                case 5:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 6:
                    applicationInfo = (ApplicationInfo) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, ApplicationInfo.CREATOR);
                    break;
                case 7:
                    packageInfo = (PackageInfo) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, PackageInfo.CREATOR);
                    break;
                case 8:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 9:
                    strZzq3 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 10:
                    strZzq4 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 11:
                    zzajeVar = (zzaje) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaje.CREATOR);
                    break;
                case 12:
                    bundleZzs2 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 13:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 14:
                    arrayListZzC = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 15:
                    bundleZzs3 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 16:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 17:
                case MotionEventCompat.AXIS_GAS /* 22 */:
                case MotionEventCompat.AXIS_BRAKE /* 23 */:
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                case 32:
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
                case 18:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 19:
                    iZzg4 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 20:
                    fZzl = com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, i);
                    break;
                case 21:
                    strZzq5 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 25:
                    jZzi = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 26:
                    strZzq6 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_RELATIVE_X /* 27 */:
                    arrayListZzC2 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case MotionEventCompat.AXIS_RELATIVE_Y /* 28 */:
                    strZzq7 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 29:
                    zzonVar = (zzon) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzon.CREATOR);
                    break;
                case 30:
                    arrayListZzC3 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case Constants.CACHE_EXPIRY_DAYS /* 31 */:
                    jZzi2 = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 33:
                    strZzq8 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    fZzl2 = com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_4 /* 35 */:
                    iZzg5 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_5 /* 36 */:
                    iZzg6 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 37:
                    zZzc3 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_7 /* 38 */:
                    zZzc4 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_8 /* 39 */:
                    strZzq9 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_10 /* 41 */:
                    strZzq10 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_11 /* 42 */:
                    zZzc5 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_12 /* 43 */:
                    iZzg7 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_13 /* 44 */:
                    bundleZzs4 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    strZzq11 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_15 /* 46 */:
                    zzkyVar = (zzky) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzky.CREATOR);
                    break;
                case MotionEventCompat.AXIS_GENERIC_16 /* 47 */:
                    zZzc6 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 48:
                    bundleZzs5 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 49:
                    strZzq12 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case AdSize.PORTRAIT_AD_HEIGHT /* 50 */:
                    strZzq13 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 51:
                    strZzq14 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 52:
                    zZzc7 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 53:
                    arrayListZzB = com.google.android.gms.common.internal.safeparcel.zzb.zzB(parcel, i);
                    break;
                case 54:
                    strZzq15 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 55:
                    arrayListZzC4 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzaae(iZzg, bundleZzs, zzirVar, zzivVar, strZzq, applicationInfo, packageInfo, strZzq2, strZzq3, strZzq4, zzajeVar, bundleZzs2, iZzg2, arrayListZzC, bundleZzs3, zZzc, iZzg3, iZzg4, fZzl, strZzq5, jZzi, strZzq6, arrayListZzC2, strZzq7, zzonVar, arrayListZzC3, jZzi2, strZzq8, fZzl2, zZzc2, iZzg5, iZzg6, zZzc3, zZzc4, strZzq9, strZzq10, zZzc5, iZzg7, bundleZzs4, strZzq11, zzkyVar, zZzc6, bundleZzs5, strZzq12, strZzq13, strZzq14, zZzc7, arrayListZzB, strZzq15, arrayListZzC4);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaae[] newArray(int i) {
        return new zzaae[i];
    }
}
