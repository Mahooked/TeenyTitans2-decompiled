package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import java.io.InputStream;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhx extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzhx> CREATOR = new zzhy();

    @Nullable
    private ParcelFileDescriptor zzzt;

    public zzhx() {
        this(null);
    }

    public zzhx(@Nullable ParcelFileDescriptor parcelFileDescriptor) {
        this.zzzt = parcelFileDescriptor;
    }

    private synchronized ParcelFileDescriptor zzda() {
        return this.zzzt;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) zzda(), i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final synchronized boolean zzcY() {
        return this.zzzt != null;
    }

    @Nullable
    public final synchronized InputStream zzcZ() {
        ParcelFileDescriptor.AutoCloseInputStream autoCloseInputStream = null;
        synchronized (this) {
            if (this.zzzt != null) {
                autoCloseInputStream = new ParcelFileDescriptor.AutoCloseInputStream(this.zzzt);
                this.zzzt = null;
            }
        }
        return autoCloseInputStream;
    }
}
