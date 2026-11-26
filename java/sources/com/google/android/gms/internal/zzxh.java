package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzxh extends zzee implements zzxg {
    public static zzxg zzs(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.purchase.client.IInAppPurchaseListener");
        return iInterfaceQueryLocalInterface instanceof zzxg ? (zzxg) iInterfaceQueryLocalInterface : new zzxi(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzxe zzxfVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        IBinder strongBinder = parcel.readStrongBinder();
        if (strongBinder == null) {
            zzxfVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.purchase.client.IInAppPurchase");
            zzxfVar = iInterfaceQueryLocalInterface instanceof zzxe ? (zzxe) iInterfaceQueryLocalInterface : new zzxf(strongBinder);
        }
        zza(zzxfVar);
        parcel2.writeNoException();
        return true;
    }
}
