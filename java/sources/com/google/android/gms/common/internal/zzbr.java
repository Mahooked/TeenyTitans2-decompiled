package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
public final class zzbr extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbr> CREATOR = new zzbs();
    private ConnectionResult zzaBQ;
    private boolean zzaDm;
    private IBinder zzaIq;
    private boolean zzaIr;
    private int zzaku;

    zzbr(int i, IBinder iBinder, ConnectionResult connectionResult, boolean z, boolean z2) {
        this.zzaku = i;
        this.zzaIq = iBinder;
        this.zzaBQ = connectionResult;
        this.zzaDm = z;
        this.zzaIr = z2;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzbr)) {
            return false;
        }
        zzbr zzbrVar = (zzbr) obj;
        return this.zzaBQ.equals(zzbrVar.zzaBQ) && zzrH().equals(zzbrVar.zzrH());
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzaIq, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) this.zzaBQ, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzaDm);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzaIr);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final ConnectionResult zzpz() {
        return this.zzaBQ;
    }

    public final zzal zzrH() {
        IBinder iBinder = this.zzaIq;
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IAccountAccessor");
        return iInterfaceQueryLocalInterface instanceof zzal ? (zzal) iInterfaceQueryLocalInterface : new zzan(iBinder);
    }

    public final boolean zzrI() {
        return this.zzaDm;
    }

    public final boolean zzrJ() {
        return this.zzaIr;
    }
}
