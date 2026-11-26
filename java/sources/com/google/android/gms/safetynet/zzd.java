package com.google.android.gms.safetynet;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzd extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzd> CREATOR = new zze();
    public final long zzbBG;
    public final HarmfulAppsData[] zzbBH;

    public zzd(long j, HarmfulAppsData[] harmfulAppsDataArr) {
        this.zzbBG = j;
        this.zzbBH = harmfulAppsDataArr;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzbBG);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable[]) this.zzbBH, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
