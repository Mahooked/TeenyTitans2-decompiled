package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzcae extends zzed implements zzcac {
    zzcae(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.flags.IFlagProvider");
    }

    @Override // com.google.android.gms.internal.zzcac
    public final boolean getBooleanFlagValue(String str, boolean z, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, z);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(2, parcelZzZ);
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzcac
    public final int getIntFlagValue(String str, int i, int i2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        parcelZzZ.writeInt(i);
        parcelZzZ.writeInt(i2);
        Parcel parcelZza = zza(3, parcelZzZ);
        int i3 = parcelZza.readInt();
        parcelZza.recycle();
        return i3;
    }

    @Override // com.google.android.gms.internal.zzcac
    public final long getLongFlagValue(String str, long j, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        parcelZzZ.writeLong(j);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(4, parcelZzZ);
        long j2 = parcelZza.readLong();
        parcelZza.recycle();
        return j2;
    }

    @Override // com.google.android.gms.internal.zzcac
    public final String getStringFlagValue(String str, String str2, int i) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        parcelZzZ.writeString(str2);
        parcelZzZ.writeInt(i);
        Parcel parcelZza = zza(5, parcelZzZ);
        String string = parcelZza.readString();
        parcelZza.recycle();
        return string;
    }

    @Override // com.google.android.gms.internal.zzcac
    public final void init(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(1, parcelZzZ);
    }
}
