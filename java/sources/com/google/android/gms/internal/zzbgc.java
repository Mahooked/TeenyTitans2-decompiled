package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgc extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbgc> CREATOR = new zzbgd();
    private final zzbge zzaIB;
    private int zzaku;

    zzbgc(int i, zzbge zzbgeVar) {
        this.zzaku = i;
        this.zzaIB = zzbgeVar;
    }

    private zzbgc(zzbge zzbgeVar) {
        this.zzaku = 1;
        this.zzaIB = zzbgeVar;
    }

    public static zzbgc zza(zzbgk<?, ?> zzbgkVar) {
        if (zzbgkVar instanceof zzbge) {
            return new zzbgc((zzbge) zzbgkVar);
        }
        throw new IllegalArgumentException("Unsupported safe parcelable field converter class.");
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzaIB, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final zzbgk<?, ?> zzrK() {
        if (this.zzaIB != null) {
            return this.zzaIB;
        }
        throw new IllegalStateException("There was no converter wrapped in this ConverterWrapper.");
    }
}
