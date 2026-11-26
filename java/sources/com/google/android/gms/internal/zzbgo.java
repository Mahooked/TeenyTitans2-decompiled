package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.zzbo;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class zzbgo extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbgo> CREATOR = new zzbgr();
    private final HashMap<String, Map<String, zzbgj<?, ?>>> zzaIR;
    private final ArrayList<zzbgp> zzaIS = null;
    private final String zzaIT;
    private int zzaku;

    zzbgo(int i, ArrayList<zzbgp> arrayList, String str) {
        this.zzaku = i;
        HashMap<String, Map<String, zzbgj<?, ?>>> map = new HashMap<>();
        int size = arrayList.size();
        for (int i2 = 0; i2 < size; i2++) {
            zzbgp zzbgpVar = arrayList.get(i2);
            map.put(zzbgpVar.className, zzbgpVar.zzrS());
        }
        this.zzaIR = map;
        this.zzaIT = (String) zzbo.zzu(str);
        zzrQ();
    }

    private final void zzrQ() {
        Iterator<String> it = this.zzaIR.keySet().iterator();
        while (it.hasNext()) {
            Map<String, zzbgj<?, ?>> map = this.zzaIR.get(it.next());
            Iterator<String> it2 = map.keySet().iterator();
            while (it2.hasNext()) {
                map.get(it2.next()).zza(this);
            }
        }
    }

    public final String toString() {
        StringBuilder sb = new StringBuilder();
        for (String str : this.zzaIR.keySet()) {
            sb.append(str).append(":\n");
            Map<String, zzbgj<?, ?>> map = this.zzaIR.get(str);
            for (String str2 : map.keySet()) {
                sb.append("  ").append(str2).append(": ");
                sb.append(map.get(str2));
            }
        }
        return sb.toString();
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        ArrayList arrayList = new ArrayList();
        for (String str : this.zzaIR.keySet()) {
            arrayList.add(new zzbgp(str, this.zzaIR.get(str)));
        }
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, arrayList, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzaIT, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final Map<String, zzbgj<?, ?>> zzcJ(String str) {
        return this.zzaIR.get(str);
    }

    public final String zzrR() {
        return this.zzaIT;
    }
}
