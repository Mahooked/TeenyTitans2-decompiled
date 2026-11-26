package com.google.android.gms.safetynet;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public class HarmfulAppsData extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<HarmfulAppsData> CREATOR = new zzc();
    public final int apkCategory;
    public final String apkPackageName;
    public final byte[] apkSha256;

    public HarmfulAppsData(String str, byte[] bArr, int i) {
        this.apkPackageName = str;
        this.apkSha256 = bArr;
        this.apkCategory = i;
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.apkPackageName, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.apkSha256, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 4, this.apkCategory);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
