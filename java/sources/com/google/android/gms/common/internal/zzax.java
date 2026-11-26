package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzax implements zzaw {
    private final IBinder zzrD;

    zzax(IBinder iBinder) {
        this.zzrD = iBinder;
    }

    @Override // android.os.IInterface
    public final IBinder asBinder() {
        return this.zzrD;
    }

    @Override // com.google.android.gms.common.internal.zzaw
    public final void zza(zzau zzauVar, zzx zzxVar) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        Parcel parcelObtain2 = Parcel.obtain();
        try {
            parcelObtain.writeInterfaceToken("com.google.android.gms.common.internal.IGmsServiceBroker");
            parcelObtain.writeStrongBinder(zzauVar.asBinder());
            parcelObtain.writeInt(1);
            zzxVar.writeToParcel(parcelObtain, 0);
            this.zzrD.transact(46, parcelObtain, parcelObtain2, 0);
            parcelObtain2.readException();
        } finally {
            parcelObtain2.recycle();
            parcelObtain.recycle();
        }
    }
}
