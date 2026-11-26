package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgf extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbgf> CREATOR = new zzbgh();
    private int versionCode;
    final String zzaIF;
    final int zzaIG;

    zzbgf(int i, String str, int i2) {
        this.versionCode = i;
        this.zzaIF = str;
        this.zzaIG = i2;
    }

    zzbgf(String str, int i) {
        this.versionCode = 1;
        this.zzaIF = str;
        this.zzaIG = i;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzaIF, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaIG);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
