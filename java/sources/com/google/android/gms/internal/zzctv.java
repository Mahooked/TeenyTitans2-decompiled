package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.zzbp;

/* JADX INFO: loaded from: classes.dex */
public final class zzctv extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzctv> CREATOR = new zzctw();
    private int zzaku;
    private zzbp zzbCU;

    zzctv(int i, zzbp zzbpVar) {
        this.zzaku = i;
        this.zzbCU = zzbpVar;
    }

    public zzctv(zzbp zzbpVar) {
        this(1, zzbpVar);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzbCU, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
