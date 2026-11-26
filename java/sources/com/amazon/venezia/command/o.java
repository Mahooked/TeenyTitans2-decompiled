package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class o extends Binder implements f {
    public o() {
        attachInterface(this, "com.amazon.venezia.command.ResultCallback");
    }

    @Override // android.os.IInterface
    public IBinder asBinder() {
        return this;
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        j vVar;
        r aaVar;
        FailureResult abVar;
        SuccessResult xVar;
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.ResultCallback");
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    xVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.amazon.venezia.command.SuccessResult");
                    xVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof SuccessResult)) ? new x(strongBinder) : (SuccessResult) iInterfaceQueryLocalInterface;
                }
                a(xVar);
                parcel2.writeNoException();
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.ResultCallback");
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 == null) {
                    abVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.amazon.venezia.command.FailureResult");
                    abVar = (iInterfaceQueryLocalInterface2 == null || !(iInterfaceQueryLocalInterface2 instanceof FailureResult)) ? new ab(strongBinder2) : (FailureResult) iInterfaceQueryLocalInterface2;
                }
                a(abVar);
                parcel2.writeNoException();
                return true;
            case 3:
                parcel.enforceInterface("com.amazon.venezia.command.ResultCallback");
                IBinder strongBinder3 = parcel.readStrongBinder();
                if (strongBinder3 == null) {
                    aaVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface3 = strongBinder3.queryLocalInterface("com.amazon.venezia.command.DecisionResult");
                    aaVar = (iInterfaceQueryLocalInterface3 == null || !(iInterfaceQueryLocalInterface3 instanceof r)) ? new aa(strongBinder3) : (r) iInterfaceQueryLocalInterface3;
                }
                a(aaVar);
                parcel2.writeNoException();
                return true;
            case 4:
                parcel.enforceInterface("com.amazon.venezia.command.ResultCallback");
                IBinder strongBinder4 = parcel.readStrongBinder();
                if (strongBinder4 == null) {
                    vVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface4 = strongBinder4.queryLocalInterface("com.amazon.venezia.command.ExceptionResult");
                    vVar = (iInterfaceQueryLocalInterface4 == null || !(iInterfaceQueryLocalInterface4 instanceof j)) ? new v(strongBinder4) : (j) iInterfaceQueryLocalInterface4;
                }
                a(vVar);
                parcel2.writeNoException();
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.ResultCallback");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
