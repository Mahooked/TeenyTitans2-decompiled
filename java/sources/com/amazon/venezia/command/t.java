package com.amazon.venezia.command;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class t implements f {
    private IBinder a;

    t(IBinder iBinder) {
        this.a = iBinder;
    }

    @Override // com.amazon.venezia.command.f
    public final void a(FailureResult failureResult) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ResultCallback");
            parcelObtain.writeStrongBinder(failureResult != null ? failureResult.asBinder() : null);
            this.a.transact(2, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }

    @Override // com.amazon.venezia.command.f
    public final void a(SuccessResult successResult) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ResultCallback");
            parcelObtain.writeStrongBinder(successResult != null ? successResult.asBinder() : null);
            this.a.transact(1, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }

    @Override // com.amazon.venezia.command.f
    public final void a(j jVar) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ResultCallback");
            parcelObtain.writeStrongBinder(jVar != null ? jVar.asBinder() : null);
            this.a.transact(4, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }

    @Override // com.amazon.venezia.command.f
    public final void a(r rVar) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.amazon.venezia.command.ResultCallback");
            parcelObtain.writeStrongBinder(rVar != null ? rVar.asBinder() : null);
            this.a.transact(3, parcelObtain, parcelObtain2, 0);
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
