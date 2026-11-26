package com.amazon.venezia.command;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class l implements h {
    private IBinder a;

    public l(IBinder iBinder) {
        this.a = iBinder;
    }

    @Override // com.amazon.venezia.command.h
    public final void a(w wVar, f fVar) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.CommandService");
            parcelObtain.writeStrongBinder(wVar != null ? wVar.asBinder() : null);
            parcelObtain.writeStrongBinder(fVar != null ? fVar.asBinder() : null);
            this.a.transact(1, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }

    @Override // android.os.IInterface
    public final IBinder asBinder() {
        return this.a;
    }
}
