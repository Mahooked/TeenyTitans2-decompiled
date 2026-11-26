package com.amazon.venezia.command;

import android.os.Binder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class g extends Binder implements y {
    public g() {
        attachInterface(this, "com.amazon.venezia.command.ChoiceContext");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.ChoiceContext");
                Map mapA = a();
                parcel2.writeNoException();
                parcel2.writeMap(mapA);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.ChoiceContext");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
