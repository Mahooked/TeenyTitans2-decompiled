package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzazv implements Parcelable.Creator<zzazu> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzazu createFromParcel(Parcel parcel) {
        zzcqn[] zzcqnVarArr = null;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        boolean zZzc = true;
        byte[][] bArrZzu = null;
        int[] iArrZzw = null;
        String[] strArrZzA = null;
        int[] iArrZzw2 = null;
        byte[] bArrZzt = null;
        zzbak zzbakVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    zzbakVar = (zzbak) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbak.CREATOR);
                    break;
                case 3:
                    bArrZzt = com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i);
                    break;
                case 4:
                    iArrZzw2 = com.google.android.gms.common.internal.safeparcel.zzb.zzw(parcel, i);
                    break;
                case 5:
                    strArrZzA = com.google.android.gms.common.internal.safeparcel.zzb.zzA(parcel, i);
                    break;
                case 6:
                    iArrZzw = com.google.android.gms.common.internal.safeparcel.zzb.zzw(parcel, i);
                    break;
                case 7:
                    bArrZzu = com.google.android.gms.common.internal.safeparcel.zzb.zzu(parcel, i);
                    break;
                case 8:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 9:
                    zzcqnVarArr = (zzcqn[]) com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i, zzcqn.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzazu(zzbakVar, bArrZzt, iArrZzw2, strArrZzA, iArrZzw, bArrZzu, zZzc, zzcqnVarArr);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzazu[] newArray(int i) {
        return new zzazu[i];
    }
}
