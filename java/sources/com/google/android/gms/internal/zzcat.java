package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzcat extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzcat> CREATOR = new zzcau();
    private String packageName;
    private int versionCode;
    private String zzbfs;

    zzcat(int i, String str, String str2) {
        this.versionCode = i;
        this.packageName = str;
        this.zzbfs = str2;
    }

    public zzcat(String str, String str2) {
        this(1, str, str2);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.packageName, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzbfs, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
