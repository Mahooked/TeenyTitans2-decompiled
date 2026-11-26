package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.ads.VideoOptions;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzlx extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzlx> CREATOR = new zzly();
    public final boolean zzBJ;
    public final boolean zzBK;

    public zzlx(VideoOptions videoOptions) {
        this(videoOptions.getStartMuted(), videoOptions.getCustomControlsRequested());
    }

    public zzlx(boolean z, boolean z2) {
        this.zzBJ = z;
        this.zzBK = z2;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzBJ);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzBK);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
