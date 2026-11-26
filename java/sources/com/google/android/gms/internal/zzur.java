package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzur extends zzee implements zzuq {
    public zzur() {
        attachInterface(this, "com.google.android.gms.ads.internal.mediation.client.IAdapterCreator");
    }

    public static zzuq zzq(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IAdapterCreator");
        return iInterfaceQueryLocalInterface instanceof zzuq ? (zzuq) iInterfaceQueryLocalInterface : new zzus(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zzut zzutVarZzah = zzah(parcel.readString());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzutVarZzah);
                return true;
            case 2:
                boolean zZzai = zzai(parcel.readString());
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzai);
                return true;
            default:
                return false;
        }
    }
}
