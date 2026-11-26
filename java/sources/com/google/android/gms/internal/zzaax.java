package com.google.android.gms.internal;

import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaax extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaax> CREATOR = new zzaay();
    private ApplicationInfo applicationInfo;
    private PackageInfo zzSA;
    private Bundle zzTU;
    private zzaje zzTV;

    public zzaax(Bundle bundle, zzaje zzajeVar, PackageInfo packageInfo, ApplicationInfo applicationInfo) {
        this.zzTU = bundle;
        this.zzTV = zzajeVar;
        this.zzSA = packageInfo;
        this.applicationInfo = applicationInfo;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 1, this.zzTU, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzTV, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) this.zzSA, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, (Parcelable) this.applicationInfo, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
