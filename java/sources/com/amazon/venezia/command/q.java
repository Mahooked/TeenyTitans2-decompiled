package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class q extends Binder implements h {
    public q() {
        attachInterface(this, "com.amazon.venezia.command.CommandService");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        w dVar;
        f tVar;
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.CommandService");
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    dVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.amazon.venezia.command.Command");
                    dVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof w)) ? new d(strongBinder) : (w) iInterfaceQueryLocalInterface;
                }
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 == null) {
                    tVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.amazon.venezia.command.ResultCallback");
                    tVar = (iInterfaceQueryLocalInterface2 == null || !(iInterfaceQueryLocalInterface2 instanceof f)) ? new t(strongBinder2) : (f) iInterfaceQueryLocalInterface2;
                }
                a(dVar, tVar);
                parcel2.writeNoException();
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.CommandService");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
