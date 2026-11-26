package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class c extends Binder implements r {
    public c() {
        attachInterface(this, "com.amazon.venezia.command.DecisionResult");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        s uVar;
        switch (i) {
            case 1:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                String strA = a();
                parcel2.writeNoException();
                parcel2.writeString(strA);
                return true;
            case 2:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                String strB = b();
                parcel2.writeNoException();
                parcel2.writeString(strB);
                return true;
            case 3:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                String strC = c();
                parcel2.writeNoException();
                parcel2.writeString(strC);
                return true;
            case 4:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                long jD = d();
                parcel2.writeNoException();
                parcel2.writeLong(jD);
                return true;
            case 5:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    uVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.amazon.venezia.command.DecisionExpirationContext");
                    uVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof s)) ? new u(strongBinder) : (s) iInterfaceQueryLocalInterface;
                }
                a(uVar);
                parcel2.writeNoException();
                return true;
            case 6:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                n nVarE = e();
                parcel2.writeNoException();
                parcel2.writeStrongBinder(nVarE != null ? nVarE.asBinder() : null);
                return true;
            case 7:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                n nVarF = f();
                parcel2.writeNoException();
                parcel2.writeStrongBinder(nVarF != null ? nVarF.asBinder() : null);
                return true;
            case 8:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                n nVarG = g();
                parcel2.writeNoException();
                parcel2.writeStrongBinder(nVarG != null ? nVarG.asBinder() : null);
                return true;
            case 9:
                parcel.enforceInterface("com.amazon.venezia.command.DecisionResult");
                Map mapH = h();
                parcel2.writeNoException();
                parcel2.writeMap(mapH);
                return true;
            case 1598968902:
                parcel2.writeString("com.amazon.venezia.command.DecisionResult");
                return true;
            default:
                return super.onTransact(i, parcel, parcel2, i2);
        }
    }
}
