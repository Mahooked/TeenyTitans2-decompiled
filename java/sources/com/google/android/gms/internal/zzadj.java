package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadj extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzadj> CREATOR = new zzadk();
    public final zzir zzSz;
    public final String zzvR;

    public zzadj(zzir zzirVar, String str) {
        this.zzSz = zzirVar;
        this.zzvR = str;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzSz, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzvR, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
