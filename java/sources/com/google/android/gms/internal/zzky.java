package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzky extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzky> CREATOR = new zzkz();
    public final int zzAR;

    public zzky(int i) {
        this.zzAR = i;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, this.zzAR);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
