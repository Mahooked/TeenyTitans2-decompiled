package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgu implements Parcelable.Creator<zzbgt> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgt createFromParcel(Parcel parcel) {
        zzbgo zzbgoVar = null;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        Parcel parcelZzD = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    parcelZzD = com.google.android.gms.common.internal.safeparcel.zzb.zzD(parcel, i);
                    break;
                case 3:
                    zzbgoVar = (zzbgo) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbgo.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzbgt(iZzg, parcelZzD, zzbgoVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgt[] newArray(int i) {
        return new zzbgt[i];
    }
}
