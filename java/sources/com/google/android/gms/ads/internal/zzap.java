package com.google.android.gms.ads.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzap extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzap> CREATOR = new zzaq();
    public final boolean zzur;
    public final boolean zzus;
    private String zzut;
    public final boolean zzuu;
    public final float zzuv;
    public final int zzuw;
    public final boolean zzux;

    zzap(boolean z, boolean z2, String str, boolean z3, float f, int i, boolean z4) {
        this.zzur = z;
        this.zzus = z2;
        this.zzut = str;
        this.zzuu = z3;
        this.zzuv = f;
        this.zzuw = i;
        this.zzux = z4;
    }

    public zzap(boolean z, boolean z2, boolean z3, float f, int i, boolean z4) {
        this(z, z2, null, z3, f, i, z4);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzur);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzus);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzut, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzuu);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzuv);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 7, this.zzuw);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzux);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
