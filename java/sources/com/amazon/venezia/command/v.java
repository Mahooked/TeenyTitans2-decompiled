package com.amazon.venezia.command;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class v implements j {
    private IBinder a;

    v(IBinder iBinder) {
        this.a = iBinder;
    }

    @Override // com.amazon.venezia.command.j
    public final String a() throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ExceptionResult");
            this.a.transact(1, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
            return parcelObtain2.readString();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }

    @Override // android.os.IInterface
    public final IBinder asBinder() {
        return this.a;
    }

    @Override // com.amazon.venezia.command.j
    public final Map b() throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ExceptionResult");
            this.a.transact(2, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
            return parcelObtain2.readHashMap(getClass().getClassLoader());
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }
}
