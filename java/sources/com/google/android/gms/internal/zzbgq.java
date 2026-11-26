package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgq extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbgq> CREATOR = new zzbgn();
    final String key;
    private int versionCode;
    final zzbgj<?, ?> zzaIV;

    zzbgq(int i, String str, zzbgj<?, ?> zzbgjVar) {
        this.versionCode = i;
        this.key = str;
        this.zzaIV = zzbgjVar;
    }

    zzbgq(String str, zzbgj<?, ?> zzbgjVar) {
        this.versionCode = 1;
        this.key = str;
        this.zzaIV = zzbgjVar;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.key, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) this.zzaIV, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
