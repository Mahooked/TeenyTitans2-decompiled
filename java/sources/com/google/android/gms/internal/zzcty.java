package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbr;

/* JADX INFO: loaded from: classes.dex */
public final class zzcty implements Parcelable.Creator<zzctx> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzctx createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        ConnectionResult connectionResult = null;
        int iZzg = 0;
        zzbr zzbrVar = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    iZzg = com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i);
                    break;
                case 2:
                    connectionResult = (ConnectionResult) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, ConnectionResult.CREATOR);
                    break;
                case 3:
                    zzbrVar = (zzbr) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzbr.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzctx(iZzg, connectionResult, zzbrVar);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzctx[] newArray(int i) {
        return new zzctx[i];
    }
}
