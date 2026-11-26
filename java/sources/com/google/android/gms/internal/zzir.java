package com.google.android.gms.internal;

import android.location.Location;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import java.util.Arrays;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzir extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzir> CREATOR = new zzit();
    public final Bundle extras;
    public final int versionCode;
    public final String zzAa;
    public final String zzAb;
    public final boolean zzAc;
    public final long zzzN;
    public final int zzzO;
    public final List<String> zzzP;
    public final boolean zzzQ;
    public final int zzzR;
    public final boolean zzzS;
    public final String zzzT;
    public final zzlt zzzU;
    public final Location zzzV;
    public final String zzzW;
    public final Bundle zzzX;
    public final Bundle zzzY;
    public final List<String> zzzZ;

    public zzir(int i, long j, Bundle bundle, int i2, List<String> list, boolean z, int i3, boolean z2, String str, zzlt zzltVar, Location location, String str2, Bundle bundle2, Bundle bundle3, List<String> list2, String str3, String str4, boolean z3) {
        this.versionCode = i;
        this.zzzN = j;
        this.extras = bundle == null ? new Bundle() : bundle;
        this.zzzO = i2;
        this.zzzP = list;
        this.zzzQ = z;
        this.zzzR = i3;
        this.zzzS = z2;
        this.zzzT = str;
        this.zzzU = zzltVar;
        this.zzzV = location;
        this.zzzW = str2;
        this.zzzX = bundle2 == null ? new Bundle() : bundle2;
        this.zzzY = bundle3;
        this.zzzZ = list2;
        this.zzAa = str3;
        this.zzAb = str4;
        this.zzAc = z3;
    }

    public static void zzh(zzir zzirVar) {
        zzirVar.zzzX.putBundle("com.google.ads.mediation.admob.AdMobAdapter", zzirVar.extras);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzir)) {
            return false;
        }
        zzir zzirVar = (zzir) obj;
        return this.versionCode == zzirVar.versionCode && this.zzzN == zzirVar.zzzN && com.google.android.gms.common.internal.zzbe.equal(this.extras, zzirVar.extras) && this.zzzO == zzirVar.zzzO && com.google.android.gms.common.internal.zzbe.equal(this.zzzP, zzirVar.zzzP) && this.zzzQ == zzirVar.zzzQ && this.zzzR == zzirVar.zzzR && this.zzzS == zzirVar.zzzS && com.google.android.gms.common.internal.zzbe.equal(this.zzzT, zzirVar.zzzT) && com.google.android.gms.common.internal.zzbe.equal(this.zzzU, zzirVar.zzzU) && com.google.android.gms.common.internal.zzbe.equal(this.zzzV, zzirVar.zzzV) && com.google.android.gms.common.internal.zzbe.equal(this.zzzW, zzirVar.zzzW) && com.google.android.gms.common.internal.zzbe.equal(this.zzzX, zzirVar.zzzX) && com.google.android.gms.common.internal.zzbe.equal(this.zzzY, zzirVar.zzzY) && com.google.android.gms.common.internal.zzbe.equal(this.zzzZ, zzirVar.zzzZ) && com.google.android.gms.common.internal.zzbe.equal(this.zzAa, zzirVar.zzAa) && com.google.android.gms.common.internal.zzbe.equal(this.zzAb, zzirVar.zzAb) && this.zzAc == zzirVar.zzAc;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.versionCode), Long.valueOf(this.zzzN), this.extras, Integer.valueOf(this.zzzO), this.zzzP, Boolean.valueOf(this.zzzQ), Integer.valueOf(this.zzzR), Boolean.valueOf(this.zzzS), this.zzzT, this.zzzU, this.zzzV, this.zzzW, this.zzzX, this.zzzY, this.zzzZ, this.zzAa, this.zzAb, Boolean.valueOf(this.zzAc)});
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzzN);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.extras, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 4, this.zzzO);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 5, this.zzzP, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzzQ);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 7, this.zzzR);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzzS);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzzT, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 10, (Parcelable) this.zzzU, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 11, (Parcelable) this.zzzV, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 12, this.zzzW, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 13, this.zzzX, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 14, this.zzzY, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 15, this.zzzZ, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 16, this.zzAa, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 17, this.zzAb, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 18, this.zzAc);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
