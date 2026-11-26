package com.google.android.gms.internal;

import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbgl extends zzbgi implements SafeParcelable {
    @Override // android.os.Parcelable
    public final int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!getClass().isInstance(obj)) {
            return false;
        }
        zzbgi zzbgiVar = (zzbgi) obj;
        for (zzbgj<?, ?> zzbgjVar : zzrL().values()) {
            if (zza(zzbgjVar)) {
                if (zzbgiVar.zza(zzbgjVar) && zzb(zzbgjVar).equals(zzbgiVar.zzb(zzbgjVar))) {
                }
                return false;
            }
            if (zzbgiVar.zza(zzbgjVar)) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        int iHashCode = 0;
        Iterator<zzbgj<?, ?>> it = zzrL().values().iterator();
        while (true) {
            int i = iHashCode;
            if (!it.hasNext()) {
                return i;
            }
            zzbgj<?, ?> next = it.next();
            if (zza(next)) {
                iHashCode = zzb(next).hashCode() + (i * 31);
            } else {
                iHashCode = i;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzbgi
    public Object zzcH(String str) {
        return null;
    }

    @Override // com.google.android.gms.internal.zzbgi
    public boolean zzcI(String str) {
        return false;
    }
}
