package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;

/* JADX INFO: loaded from: classes.dex */
public final class zzcav extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzcav> CREATOR = new zzcaw();
    private int versionCode;
    private zzax zzbft = null;
    private byte[] zzbfu;

    zzcav(int i, byte[] bArr) {
        this.versionCode = i;
        this.zzbfu = bArr;
        zzsA();
    }

    private final void zzsA() {
        if (this.zzbft != null || this.zzbfu == null) {
            if (this.zzbft == null || this.zzbfu != null) {
                if (this.zzbft != null && this.zzbfu != null) {
                    throw new IllegalStateException("Invalid internal representation - full");
                }
                if (this.zzbft != null || this.zzbfu != null) {
                    throw new IllegalStateException("Impossible");
                }
                throw new IllegalStateException("Invalid internal representation - empty");
            }
        }
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzbfu != null ? this.zzbfu : adp.zzc(this.zzbft), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final zzax zzvA() {
        if (!(this.zzbft != null)) {
            try {
                this.zzbft = (zzax) adp.zza(new zzax(), this.zzbfu);
                this.zzbfu = null;
            } catch (ado e) {
                throw new IllegalStateException(e);
            }
        }
        zzsA();
        return this.zzbft;
    }
}
