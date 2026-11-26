package com.amazon.venezia.command;

import android.os.Binder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class b extends Binder implements FailureResult {
    public b() {
        attachInterface(this, "com.amazon.venezia.command.FailureResult");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                String authToken = getAuthToken();
                parcel2.writeNoException();
                parcel2.writeString(authToken);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                String displayableName = getDisplayableName();
                parcel2.writeNoException();
                parcel2.writeString(displayableName);
                return true;
            case 3:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                String displayableMessage = getDisplayableMessage();
                parcel2.writeNoException();
                parcel2.writeString(displayableMessage);
                return true;
            case 4:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                String buttonLabel = getButtonLabel();
                parcel2.writeNoException();
                parcel2.writeString(buttonLabel);
                return true;
            case 5:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                boolean zShow = show();
                parcel2.writeNoException();
                parcel2.writeInt(zShow ? 1 : 0);
                return true;
            case 6:
                parcel.enforceInterface("com.amazon.venezia.command.FailureResult");
                Map extensionData = getExtensionData();
                parcel2.writeNoException();
                parcel2.writeMap(extensionData);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.FailureResult");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
