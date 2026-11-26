package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgm implements Parcelable.Creator<zzbgj> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgj createFromParcel(Parcel parcel) {
        zzbgc zzbgcVar = null;
        int iZzg = 0;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        String strZzq = null;
        String strZzq2 = null;
        boolean zZzc = false;
        int iZzg2 = 0;
        boolean zZzc2 = false;
        int iZzg3 = 0;
        int iZzg4 = 0;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg4 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 3:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 4:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 5:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 6:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 7:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 8:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 9:
                    zzbgcVar = (zzbgc) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbgc.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzbgj(iZzg4, iZzg3, zZzc2, iZzg2, zZzc, strZzq2, iZzg, strZzq, zzbgcVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgj[] newArray(int i) {
        return new zzbgj[i];
    }
}
