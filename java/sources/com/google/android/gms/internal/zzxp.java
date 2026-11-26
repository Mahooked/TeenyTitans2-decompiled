package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzxp extends zzee implements zzxo {
    public static zzxo zzu(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.purchase.client.IPlayStorePurchaseListener");
        return iInterfaceQueryLocalInterface instanceof zzxo ? (zzxo) iInterfaceQueryLocalInterface : new zzxq(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzxm zzxnVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                boolean zZzas = zzas(parcel.readString());
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzas);
                break;
            case 2:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    zzxnVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.purchase.client.IInAppPurchaseResult");
                    zzxnVar = iInterfaceQueryLocalInterface instanceof zzxm ? (zzxm) iInterfaceQueryLocalInterface : new zzxn(strongBinder);
                }
                zza(zzxnVar);
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
