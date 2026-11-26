package com.amazon.venezia.command;

import android.os.Binder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class e extends Binder implements SuccessResult {
    public e() {
        attachInterface(this, "com.amazon.venezia.command.SuccessResult");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.SuccessResult");
                String authToken = getAuthToken();
                parcel2.writeNoException();
                parcel2.writeString(authToken);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.SuccessResult");
                Map data = getData();
                parcel2.writeNoException();
                parcel2.writeMap(data);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.SuccessResult");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
