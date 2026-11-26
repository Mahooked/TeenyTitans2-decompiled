package com.google.android.gms.internal;

import android.os.Parcel;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzlv extends zziv {
    public zzlv(zziv zzivVar) {
        super(zzivVar.zzAs, zzivVar.height, zzivVar.heightPixels, zzivVar.zzAt, zzivVar.width, zzivVar.widthPixels, zzivVar.zzAu, zzivVar.zzAv, zzivVar.zzAw, zzivVar.zzAx);
    }

    @Override // com.google.android.gms.internal.zziv, android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzAs, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.height);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 6, this.width);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
