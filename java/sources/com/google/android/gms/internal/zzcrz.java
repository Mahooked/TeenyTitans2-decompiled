package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzcrz extends zzed implements zzcry {
    zzcrz(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.safetynet.internal.ISafetyNetService");
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zzAj() throws RemoteException {
        zzb(13, zzZ());
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zza(zzcrw zzcrwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        zzb(12, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zza(zzcrw zzcrwVar, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        parcelZzZ.writeString(str);
        zzb(6, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zza(zzcrw zzcrwVar, String str, int[] iArr, int i, String str2) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        parcelZzZ.writeString(str);
        parcelZzZ.writeIntArray(iArr);
        parcelZzZ.writeInt(i);
        parcelZzZ.writeString(str2);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zza(zzcrw zzcrwVar, byte[] bArr, String str) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        parcelZzZ.writeByteArray(bArr);
        parcelZzZ.writeString(str);
        zzb(7, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zzb(zzcrw zzcrwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        zzb(14, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zzc(zzcrw zzcrwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzcry
    public final void zzd(zzcrw zzcrwVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzcrwVar);
        zzb(5, parcelZzZ);
    }
}
