package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzlu implements Parcelable.Creator<zzlt> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzlt createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        int iZzg2 = 0;
        int iZzg3 = 0;
        int iZzg4 = 0;
        int iZzg5 = 0;
        int iZzg6 = 0;
        int iZzg7 = 0;
        int iZzg8 = 0;
        String strZzq = null;
        int iZzg9 = 0;
        String strZzq2 = null;
        int iZzg10 = 0;
        int iZzg11 = 0;
        String strZzq3 = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 3:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 4:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 5:
                    iZzg4 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 6:
                    iZzg5 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 7:
                    iZzg6 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 8:
                    iZzg7 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 9:
                    iZzg8 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 10:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 11:
                    iZzg9 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 12:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 13:
                    iZzg10 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 14:
                    iZzg11 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 15:
                    strZzq3 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzlt(iZzg, iZzg2, iZzg3, iZzg4, iZzg5, iZzg6, iZzg7, iZzg8, strZzq, iZzg9, strZzq2, iZzg10, iZzg11, strZzq3);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzlt[] newArray(int i) {
        return new zzlt[i];
    }
}
