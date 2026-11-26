package com.google.android.gms.common;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.common.internal.zzas;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzm extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzm> CREATOR = new zzn();
    private final String zzaAl;
    private final zzg zzaAm;
    private final boolean zzaAn;

    zzm(String str, IBinder iBinder, boolean z) {
        this.zzaAl = str;
        this.zzaAm = zzG(iBinder);
        this.zzaAn = z;
    }

    zzm(String str, zzg zzgVar, boolean z) {
        this.zzaAl = str;
        this.zzaAm = zzgVar;
        this.zzaAn = z;
    }

    private static zzg zzG(IBinder iBinder) {
        zzh zzhVar;
        if (iBinder == null) {
            return null;
        }
        try {
            IObjectWrapper iObjectWrapperZzoY = zzas.zzI(iBinder).zzoY();
            byte[] bArr = iObjectWrapperZzoY == null ? null : (byte[]) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzoY);
            if (bArr != null) {
                zzhVar = new zzh(bArr);
            } else {
                Log.e("GoogleCertificatesQuery", "Could not unwrap certificate");
                zzhVar = null;
            }
            return zzhVar;
        } catch (RemoteException e) {
            Log.e("GoogleCertificatesQuery", "Could not unwrap certificate", e);
            return null;
        }
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinderAsBinder;
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 1, this.zzaAl, false);
        if (this.zzaAm == null) {
            Log.w("GoogleCertificatesQuery", "certificate binder is null");
            iBinderAsBinder = null;
        } else {
            iBinderAsBinder = this.zzaAm.asBinder();
        }
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, iBinderAsBinder, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzaAn);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
