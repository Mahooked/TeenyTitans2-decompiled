package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zzaal implements Parcelable.Creator<zzaak> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaak createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        boolean zZzc = false;
        ArrayList<String> arrayListZzC = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 2:
                    zZzc = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i);
                    break;
                case 3:
                    arrayListZzC = com.google.android.gms.common.internal.safeparcel.zzb.zzC(parcel, i);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzaak(zZzc, arrayListZzC);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaak[] newArray(int i) {
        return new zzaak[i];
    }
}
