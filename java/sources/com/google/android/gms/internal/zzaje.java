package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaje extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaje> CREATOR = new zzajf();
    public String zzaP;
    public int zzaaO;
    public int zzaaP;
    public boolean zzaaQ;
    public boolean zzaaR;

    public zzaje(int i, int i2, boolean z) {
        this(i, i2, z, false, false);
    }

    public zzaje(int i, int i2, boolean z, boolean z2) {
        this(11020000, i2, true, false, z2);
    }

    /* JADX WARN: Illegal instructions before constructor call */
    private zzaje(int i, int i2, boolean z, boolean z2, boolean z3) {
        String strValueOf = String.valueOf("afma-sdk-a-v");
        String str = z ? "0" : "1";
        this(new StringBuilder(String.valueOf(strValueOf).length() + 24 + String.valueOf(str).length()).append(strValueOf).append(i).append(".").append(i2).append(".").append(str).toString(), i, i2, z, z3);
    }

    zzaje(String str, int i, int i2, boolean z, boolean z2) {
        this.zzaP = str;
        this.zzaaO = i;
        this.zzaaP = i2;
        this.zzaaQ = z;
        this.zzaaR = z2;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzaP, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaaO);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 4, this.zzaaP);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzaaQ);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzaaR);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
