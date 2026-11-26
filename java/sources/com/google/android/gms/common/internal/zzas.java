package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzef;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzas extends zzee implements zzar {
    public zzas() {
        attachInterface(this, "com.google.android.gms.common.internal.ICertData");
    }

    public static zzar zzI(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.ICertData");
        return iInterfaceQueryLocalInterface instanceof zzar ? (zzar) iInterfaceQueryLocalInterface : new zzat(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                IObjectWrapper iObjectWrapperZzoY = zzoY();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzoY);
                return true;
            case 2:
                int iZzoZ = zzoZ();
                parcel2.writeNoException();
                parcel2.writeInt(iZzoZ);
                return true;
            default:
                return false;
        }
    }
}
