package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zziw implements Parcelable.Creator<zziv> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zziv createFromParcel(Parcel parcel) {
        zziv[] zzivVarArr = null;
        boolean zZzc = false;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        boolean zZzc2 = false;
        boolean zZzc3 = false;
        int iZzg = 0;
        int iZzg2 = 0;
        boolean zZzc4 = false;
        int iZzg3 = 0;
        int iZzg4 = 0;
        String strZzq = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 3:
                    iZzg4 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 4:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 5:
                    zZzc4 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 6:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 7:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 8:
                    zzivVarArr = (zziv[]) com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i, zziv.CREATOR);
                    break;
                case 9:
                    zZzc3 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 10:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 11:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zziv(strZzq, iZzg4, iZzg3, zZzc4, iZzg2, iZzg, zzivVarArr, zZzc3, zZzc2, zZzc);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zziv[] newArray(int i) {
        return new zziv[i];
    }
}
