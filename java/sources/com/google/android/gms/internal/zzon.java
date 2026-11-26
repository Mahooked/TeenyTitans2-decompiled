package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.formats.NativeAdOptions;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzon extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzon> CREATOR = new zzoo();
    public final int versionCode;
    public final boolean zzIn;
    public final int zzIo;
    public final boolean zzIp;
    public final int zzIq;

    @Nullable
    public final zzlx zzIr;

    public zzon(int i, boolean z, int i2, boolean z2, int i3, zzlx zzlxVar) {
        this.versionCode = i;
        this.zzIn = z;
        this.zzIo = i2;
        this.zzIp = z2;
        this.zzIq = i3;
        this.zzIr = zzlxVar;
    }

    public zzon(NativeAdOptions nativeAdOptions) {
        this(3, nativeAdOptions.shouldReturnUrlsForImageAssets(), nativeAdOptions.getImageOrientation(), nativeAdOptions.shouldRequestMultipleImages(), nativeAdOptions.getAdChoicesPlacement(), nativeAdOptions.getVideoOptions() != null ? new zzlx(nativeAdOptions.getVideoOptions()) : null);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzIn);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzIo);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzIp);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 5, this.zzIq);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, (Parcelable) this.zzIr, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
