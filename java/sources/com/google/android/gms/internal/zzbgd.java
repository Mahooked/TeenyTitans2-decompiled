package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgd implements Parcelable.Creator<zzbgc> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgc createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        zzbge zzbgeVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    zzbgeVar = (zzbge) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbge.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzbgc(iZzg, zzbgeVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgc[] newArray(int i) {
        return new zzbgc[i];
    }
}
