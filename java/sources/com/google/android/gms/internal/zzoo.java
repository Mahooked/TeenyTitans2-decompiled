package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzoo implements Parcelable.Creator<zzon> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzon createFromParcel(Parcel parcel) {
        int iZzg = 0;
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        zzlx zzlxVar = null;
        boolean zZzc = false;
        int iZzg2 = 0;
        boolean zZzc2 = false;
        int iZzg3 = 0;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg3 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    zZzc2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 3:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 4:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 5:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 6:
                    zzlxVar = (zzlx) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzlx.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzon(iZzg3, zZzc2, iZzg2, zZzc, iZzg, zzlxVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzon[] newArray(int i) {
        return new zzon[i];
    }
}
