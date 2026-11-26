package com.amazon.venezia.command;

import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class m extends Binder implements n {
    public m() {
        attachInterface(this, "com.amazon.venezia.command.Choice");
    }

    public static n a(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.amazon.venezia.command.Choice");
        return (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof n)) ? new a(iBinder) : (n) iInterfaceQueryLocalInterface;
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        y kVar;
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.Choice");
                String strA = a();
                parcel2.writeNoException();
                parcel2.writeString(strA);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.Choice");
                Intent intentB = b();
                parcel2.writeNoException();
                if (intentB != null) {
                    parcel2.writeInt(1);
                    intentB.writeToParcel(parcel2, 1);
                } else {
                    parcel2.writeInt(0);
                }
                return true;
            case 3:
                parcel.enforceInterface("com.amazon.venezia.command.Choice");
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    kVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.amazon.venezia.command.ChoiceContext");
                    kVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof y)) ? new k(strongBinder) : (y) iInterfaceQueryLocalInterface;
                }
                a(kVar);
                parcel2.writeNoException();
                return true;
            case 4:
                parcel.enforceInterface("com.amazon.venezia.command.Choice");
                Map mapC = c();
                parcel2.writeNoException();
                parcel2.writeMap(mapC);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.Choice");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
