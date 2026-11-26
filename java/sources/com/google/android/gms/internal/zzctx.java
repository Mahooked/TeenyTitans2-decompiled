package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbr;

/* JADX INFO: loaded from: classes.dex */
public final class zzctx extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzctx> CREATOR = new zzcty();
    private final ConnectionResult zzaBQ;
    private int zzaku;
    private final zzbr zzbCV;

    public zzctx(int i) {
        this(new ConnectionResult(8, null), null);
    }

    zzctx(int i, ConnectionResult connectionResult, zzbr zzbrVar) {
        this.zzaku = i;
        this.zzaBQ = connectionResult;
        this.zzbCV = zzbrVar;
    }

    private zzctx(ConnectionResult connectionResult, zzbr zzbrVar) {
        this(1, connectionResult, null);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzaBQ, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) this.zzbCV, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final zzbr zzAx() {
        return this.zzbCV;
    }

    public final ConnectionResult zzpz() {
        return this.zzaBQ;
    }
}
