package com.google.android.gms.safetynet;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzc implements Parcelable.Creator<HarmfulAppsData> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ HarmfulAppsData createFromParcel(Parcel parcel) {
        byte[] bArrZzt = null;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        String strZzq = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 3:
                    bArrZzt = com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i);
                    break;
                case 4:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new HarmfulAppsData(strZzq, bArrZzt, iZzg);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ HarmfulAppsData[] newArray(int i) {
        return new HarmfulAppsData[i];
    }
}
