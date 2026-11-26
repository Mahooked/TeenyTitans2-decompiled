package com.google.android.gms.safetynet;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.data.DataHolder;

/* JADX INFO: loaded from: classes.dex */
public class SafeBrowsingData extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<SafeBrowsingData> CREATOR = new zzh();
    private String zzbBI;
    private DataHolder zzbBJ;

    public SafeBrowsingData(String str) {
        this(str, null);
    }

    public SafeBrowsingData(String str, DataHolder dataHolder) {
        this.zzbBI = str;
        this.zzbBJ = dataHolder;
    }

    public DataHolder getBlacklistsDataHolder() {
        return this.zzbBJ;
    }

    public String getMetadata() {
        return this.zzbBI;
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, getMetadata(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) getBlacklistsDataHolder(), i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
