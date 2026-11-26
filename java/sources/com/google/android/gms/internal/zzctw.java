package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.zzbp;

/* JADX INFO: loaded from: classes.dex */
public final class zzctw implements Parcelable.Creator<zzctv> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzctv createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        int iZzg = 0;
        zzbp zzbpVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    zzbpVar = (zzbp) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbp.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzctv(iZzg, zzbpVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzctv[] newArray(int i) {
        return new zzctv[i];
    }
}
