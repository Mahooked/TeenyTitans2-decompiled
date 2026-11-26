package com.google.android.gms.internal;

import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzaay implements Parcelable.Creator<zzaax> {
    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaax createFromParcel(Parcel parcel) {
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        ApplicationInfo applicationInfo = null;
        PackageInfo packageInfo = null;
        zzaje zzajeVar = null;
        Bundle bundleZzs = null;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            switch (65535 & i) {
                case 1:
                    bundleZzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    break;
                case 2:
                    zzajeVar = (zzaje) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, zzaje.CREATOR);
                    break;
                case 3:
                    packageInfo = (PackageInfo) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, PackageInfo.CREATOR);
                    break;
                case 4:
                    applicationInfo = (ApplicationInfo) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i, ApplicationInfo.CREATOR);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, i);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzF(parcel, iZzd);
        return new zzaax(bundleZzs, zzajeVar, packageInfo, applicationInfo);
    }

    @Override // android.os.Parcelable.Creator
    public final /* synthetic */ zzaax[] newArray(int i) {
        return new zzaax[i];
    }
}
