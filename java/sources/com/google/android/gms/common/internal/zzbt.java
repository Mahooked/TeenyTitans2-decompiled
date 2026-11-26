package com.google.android.gms.common.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.api.Scope;

/* JADX INFO: loaded from: classes.dex */
public final class zzbt extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbt> CREATOR = new zzbu();
    private final int zzaIs;
    private final int zzaIt;

    @Deprecated
    private final Scope[] zzaIu;
    private int zzaku;

    zzbt(int i, int i2, int i3, Scope[] scopeArr) {
        this.zzaku = i;
        this.zzaIs = i2;
        this.zzaIt = i3;
        this.zzaIu = scopeArr;
    }

    public zzbt(int i, int i2, Scope[] scopeArr) {
        this(1, i, i2, null);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, this.zzaIs);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaIt);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, (Parcelable[]) this.zzaIu, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
