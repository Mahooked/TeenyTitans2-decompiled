package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class z extends Binder implements w {
    public z() {
        attachInterface(this, "com.amazon.venezia.command.Command");
    }

    @Override // android.os.IInterface
    public IBinder asBinder() {
        return this;
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.Command");
                String strA = a();
                parcel2.writeNoException();
                parcel2.writeString(strA);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.Command");
                String strB = b();
                parcel2.writeNoException();
                parcel2.writeString(strB);
                return true;
            case 3:
                parcel.enforceInterface("com.amazon.venezia.command.Command");
                String strC = c();
                parcel2.writeNoException();
                parcel2.writeString(strC);
                return true;
            case 4:
                parcel.enforceInterface("com.amazon.venezia.command.Command");
                Map mapD = d();
                parcel2.writeNoException();
                parcel2.writeMap(mapD);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.Command");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
