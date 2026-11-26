package com.google.android.gms.ads.internal.overlay;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.internal.zzaje;

/* JADX INFO: loaded from: classes.dex */
public final class zzv implements Parcelable.Creator<AdOverlayInfoParcel> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ AdOverlayInfoParcel createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        zzc zzcVar = null;
        IBinder iBinderZzr = null;
        IBinder iBinderZzr2 = null;
        IBinder iBinderZzr3 = null;
        IBinder iBinderZzr4 = null;
        String strZzq = null;
        boolean zZzc = false;
        String strZzq2 = null;
        IBinder iBinderZzr5 = null;
        int iZzg = 0;
        int iZzg2 = 0;
        String strZzq3 = null;
        zzaje zzajeVar = null;
        String strZzq4 = null;
        com.google.android.gms.ads.internal.zzap zzapVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    zzcVar = (zzc) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzc.CREATOR);
                    break;
                case 3:
                    iBinderZzr = com.google.android.gms.common.internal.safeparcel.zzb.zzr(parcel, i);
                    break;
                case 4:
                    iBinderZzr2 = com.google.android.gms.common.internal.safeparcel.zzb.zzr(parcel, i);
                    break;
                case 5:
                    iBinderZzr3 = com.google.android.gms.common.internal.safeparcel.zzb.zzr(parcel, i);
                    break;
                case 6:
                    iBinderZzr4 = com.google.android.gms.common.internal.safeparcel.zzb.zzr(parcel, i);
                    break;
                case 7:
                    strZzq = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 8:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 9:
                    strZzq2 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 10:
                    iBinderZzr5 = com.google.android.gms.common.internal.safeparcel.zzb.zzr(parcel, i);
                    break;
                case 11:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 12:
                    iZzg2 = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 13:
                    strZzq3 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 14:
                    zzajeVar = (zzaje) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaje.CREATOR);
                    break;
                case 15:
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
                case 16:
                    strZzq4 = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i);
                    break;
                case 17:
                    zzapVar = (com.google.android.gms.ads.internal.zzap) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, com.google.android.gms.ads.internal.zzap.CREATOR);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new AdOverlayInfoParcel(zzcVar, iBinderZzr, iBinderZzr2, iBinderZzr3, iBinderZzr4, strZzq, zZzc, strZzq2, iBinderZzr5, iZzg, iZzg2, strZzq3, zzajeVar, strZzq4, zzapVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ AdOverlayInfoParcel[] newArray(int i) {
        return new AdOverlayInfoParcel[i];
    }
}
