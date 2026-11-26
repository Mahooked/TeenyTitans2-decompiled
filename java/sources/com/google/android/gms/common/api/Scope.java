package com.google.android.gms.common.api;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
public final class Scope extends com.google.android.gms.common.internal.safeparcel.zza implements ReflectedParcelable {
    public static final Parcelable.Creator<Scope> CREATOR = new zze();
    private final String zzaBl;
    private int zzaku;

    Scope(int i, String str) {
        zzbo.zzh(str, "scopeUri must not be null or empty");
        this.zzaku = i;
        this.zzaBl = str;
    }

    public Scope(String str) {
        this(1, str);
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj instanceof Scope) {
            return this.zzaBl.equals(((Scope) obj).zzaBl);
        }
        return false;
    }

    public final int hashCode() {
        return this.zzaBl.hashCode();
    }

    public final String toString() {
        return this.zzaBl;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzaBl, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final String zzpp() {
        return this.zzaBl;
    }
}
