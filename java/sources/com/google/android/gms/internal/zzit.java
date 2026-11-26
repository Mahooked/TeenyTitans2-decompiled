package com.google.android.gms.internal;

import android.location.Location;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zzit implements Parcelable.Creator<zzir> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzir createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        long jZzi = 0;
        Bundle bundleZzs = null;
        int iZzg2 = 0;
        ArrayList<String> arrayListZzC = null;
        boolean zZzc = false;
        int iZzg3 = 0;
        boolean zZzc2 = false;
        String strZzq = null;
        zzlt zzltVar = null;
        Location location = null;
        String strZzq2 = null;
        Bundle bundleZzs2 = null;
        Bundle bundleZzs3 = null;
        ArrayList<String> arrayListZzC2 = null;
        String strZzq3 = null;
        String strZzq4 = null;
        boolean zZzc3 = false;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    jZzi = com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i);
                    break;
                case 3:
                    bundleZzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 4:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 5:
                    arrayListZzC = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 6:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 7:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 8:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 9:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 10:
                    zzltVar = (zzlt) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzlt.CREATOR);
                    break;
                case 11:
                    location = (Location) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, Location.CREATOR);
                    break;
                case 12:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 13:
                    bundleZzs2 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 14:
                    bundleZzs3 = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 15:
                    arrayListZzC2 = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                case 16:
                    strZzq3 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 17:
                    strZzq4 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 18:
                    zZzc3 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzir(iZzg, jZzi, bundleZzs, iZzg2, arrayListZzC, zZzc, iZzg3, zZzc2, strZzq, zzltVar, location, strZzq2, bundleZzs2, bundleZzs3, arrayListZzC2, strZzq3, strZzq4, zZzc3);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzir[] newArray(int i) {
        return new zzir[i];
    }
}
