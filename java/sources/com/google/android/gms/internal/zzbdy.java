package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
public final class zzbdy<L> {
    private final L mListener;
    private final String zzaEP;

    zzbdy(L l, String str) {
        this.mListener = l;
        this.zzaEP = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzbdy)) {
            return false;
        }
        zzbdy zzbdyVar = (zzbdy) obj;
        return this.mListener == zzbdyVar.mListener && this.zzaEP.equals(zzbdyVar.zzaEP);
    }

    public final int hashCode() {
        return (System.identityHashCode(this.mListener) * 31) + this.zzaEP.hashCode();
    }
}
