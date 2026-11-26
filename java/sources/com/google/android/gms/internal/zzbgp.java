package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgp extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbgp> CREATOR = new zzbgs();
    final String className;
    private int versionCode;
    private ArrayList<zzbgq> zzaIU;

    zzbgp(int i, String str, ArrayList<zzbgq> arrayList) {
        this.versionCode = i;
        this.className = str;
        this.zzaIU = arrayList;
    }

    zzbgp(String str, Map<String, zzbgj<?, ?>> map) {
        ArrayList<zzbgq> arrayList;
        this.versionCode = 1;
        this.className = str;
        if (map == null) {
            arrayList = null;
        } else {
            ArrayList<zzbgq> arrayList2 = new ArrayList<>();
            for (String str2 : map.keySet()) {
                arrayList2.add(new zzbgq(str2, map.get(str2)));
            }
            arrayList = arrayList2;
        }
        this.zzaIU = arrayList;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.className, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaIU, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    final HashMap<String, zzbgj<?, ?>> zzrS() {
        HashMap<String, zzbgj<?, ?>> map = new HashMap<>();
        int size = this.zzaIU.size();
        for (int i = 0; i < size; i++) {
            zzbgq zzbgqVar = this.zzaIU.get(i);
            map.put(zzbgqVar.key, zzbgqVar.zzaIV);
        }
        return map;
    }
}
