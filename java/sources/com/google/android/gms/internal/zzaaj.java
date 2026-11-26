package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.v4.view.MotionEventCompat;
import com.comscore.utils.Constants;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zzaaj implements Parcelable.Creator<zzaai> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaai createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        String strZzq = null;
        String strZzq2 = null;
        ArrayList<String> arrayListZzC = null;
        int iZzg2 = 0;
        ArrayList<String> arrayListZzC2 = null;
        long jZzi = 0;
        boolean zZzc = false;
        long jZzi2 = 0;
        ArrayList<String> arrayListZzC3 = null;
        long jZzi3 = 0;
        int iZzg3 = 0;
        String strZzq3 = null;
        long jZzi4 = 0;
        String strZzq4 = null;
        boolean zZzc2 = false;
        String strZzq5 = null;
        String strZzq6 = null;
        boolean zZzc3 = false;
        boolean zZzc4 = false;
        boolean zZzc5 = false;
        boolean zZzc6 = false;
        boolean zZzc7 = false;
        zzaau zzaauVar = null;
        String strZzq7 = null;
        String strZzq8 = null;
        boolean zZzc8 = false;
        boolean zZzc9 = false;
        zzaee zzaeeVar = null;
        ArrayList<String> arrayListZzC4 = null;
        ArrayList<String> arrayListZzC5 = null;
        boolean zZzc10 = false;
        zzaak zzaakVar = null;
        boolean zZzc11 = false;
        String strZzq9 = null;
        ArrayList<String> arrayListZzC6 = null;
        boolean zZzc12 = false;
        String strZzq10 = null;
        zzaeq zzaeqVar = null;
        String strZzq11 = null;
        boolean zZzc13 = false;
        boolean zZzc14 = false;
        Bundle bundleZzs = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 3:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 4:
                    arrayListZzC = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 5:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 6:
                    arrayListZzC2 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 7:
                    jZzi = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 8:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 9:
                    jZzi2 = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 10:
                    arrayListZzC3 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 11:
                    jZzi3 = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 12:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 13:
                    strZzq3 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 14:
                    jZzi4 = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 15:
                    strZzq4 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 16:
                case 17:
                case 20:
                case MotionEventCompat.AXIS_RELATIVE_X /* 27 */:
                case MotionEventCompat.AXIS_GENERIC_10 /* 41 */:
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
                case 18:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 19:
                    strZzq5 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 21:
                    strZzq6 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GAS /* 22 */:
                    zZzc3 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_BRAKE /* 23 */:
                    zZzc4 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    zZzc5 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 25:
                    zZzc6 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 26:
                    zZzc7 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_RELATIVE_Y /* 28 */:
                    zzaauVar = (zzaau) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaau.CREATOR);
                    break;
                case 29:
                    strZzq7 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 30:
                    strZzq8 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case Constants.CACHE_EXPIRY_DAYS /* 31 */:
                    zZzc8 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 32:
                    zZzc9 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 33:
                    zzaeeVar = (zzaee) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaee.CREATOR);
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    arrayListZzC4 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_4 /* 35 */:
                    arrayListZzC5 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_5 /* 36 */:
                    zZzc10 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 37:
                    zzaakVar = (zzaak) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaak.CREATOR);
                    break;
                case MotionEventCompat.AXIS_GENERIC_7 /* 38 */:
                    zZzc11 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_8 /* 39 */:
                    strZzq9 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    arrayListZzC6 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_11 /* 42 */:
                    zZzc12 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_12 /* 43 */:
                    strZzq10 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_13 /* 44 */:
                    zzaeqVar = (zzaeq) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaeq.CREATOR);
                    break;
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    strZzq11 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_15 /* 46 */:
                    zZzc13 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case MotionEventCompat.AXIS_GENERIC_16 /* 47 */:
                    zZzc14 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 48:
                    bundleZzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzaai(iZzg, strZzq, strZzq2, arrayListZzC, iZzg2, arrayListZzC2, jZzi, zZzc, jZzi2, arrayListZzC3, jZzi3, iZzg3, strZzq3, jZzi4, strZzq4, zZzc2, strZzq5, strZzq6, zZzc3, zZzc4, zZzc5, zZzc6, zZzc7, zzaauVar, strZzq7, strZzq8, zZzc8, zZzc9, zzaeeVar, arrayListZzC4, arrayListZzC5, zZzc10, zzaakVar, zZzc11, strZzq9, arrayListZzC6, zZzc12, strZzq10, zzaeqVar, strZzq11, zZzc13, zZzc14, bundleZzs);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaai[] newArray(int i) {
        return new zzaai[i];
    }
}
