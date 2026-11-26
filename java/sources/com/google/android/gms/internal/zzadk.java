package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzadk implements Parcelable.Creator<zzadj> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzadj createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        String strZzq = null;
        zzir zzirVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    zzirVar = (zzir) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzir.CREATOR);
                    break;
                case 3:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzadj(zzirVar, strZzq);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzadj[] newArray(int i) {
        return new zzadj[i];
    }
}
