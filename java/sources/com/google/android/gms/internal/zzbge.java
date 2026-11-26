package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.util.SparseArray;
import java.util.ArrayList;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzbge extends com.google.android.gms.common.internal.safeparcel.zza implements zzbgk<String, Integer> {
    public static final Parcelable.Creator<zzbge> CREATOR = new zzbgg();
    private final HashMap<String, Integer> zzaIC;
    private final SparseArray<String> zzaID;
    private final ArrayList<zzbgf> zzaIE;
    private int zzaku;

    public zzbge() {
        this.zzaku = 1;
        this.zzaIC = new HashMap<>();
        this.zzaID = new SparseArray<>();
        this.zzaIE = null;
    }

    zzbge(int i, ArrayList<zzbgf> arrayList) {
        this.zzaku = i;
        this.zzaIC = new HashMap<>();
        this.zzaID = new SparseArray<>();
        this.zzaIE = null;
        zzd(arrayList);
    }

    private final void zzd(ArrayList<zzbgf> arrayList) {
        ArrayList<zzbgf> arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            zzbgf zzbgfVar = arrayList2.get(i);
            i++;
            zzbgf zzbgfVar2 = zzbgfVar;
            zzi(zzbgfVar2.zzaIF, zzbgfVar2.zzaIG);
        }
    }

    @Override // com.google.android.gms.internal.zzbgk
    public final /* synthetic */ String convertBack(Integer num) {
        String str = this.zzaID.get(num.intValue());
        return (str == null && this.zzaIC.containsKey("gms_unknown")) ? "gms_unknown" : str;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        ArrayList arrayList = new ArrayList();
        for (String str : this.zzaIC.keySet()) {
            arrayList.add(new zzbgf(str, this.zzaIC.get(str).intValue()));
        }
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, arrayList, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final zzbge zzi(String str, int i) {
        this.zzaIC.put(str, Integer.valueOf(i));
        this.zzaID.put(i, str);
        return this;
    }
}
