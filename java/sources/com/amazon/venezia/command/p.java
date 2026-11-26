package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class p extends Binder implements s {
    public p() {
        attachInterface(this, "com.amazon.venezia.command.DecisionExpirationContext");
    }

    @Override // android.os.IInterface
    public IBinder asBinder() {
        return this;
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionExpirationContext");
                String strA = a();
                parcel2.writeNoException();
                parcel2.writeString(strA);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionExpirationContext");
                Map mapB = b();
                parcel2.writeNoException();
                parcel2.writeMap(mapB);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.DecisionExpirationContext");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
