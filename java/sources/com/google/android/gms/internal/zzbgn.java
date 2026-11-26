package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgn implements Parcelable.Creator<zzbgq> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgq createFromParcel(Parcel parcel) {
        zzbgj zzbgjVar = null;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        String strZzq = null;
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
                    zzbgjVar = (zzbgj) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbgj.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzbgq(iZzg, strZzq, zzbgjVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzbgq[] newArray(int i) {
        return new zzbgq[i];
    }
}
