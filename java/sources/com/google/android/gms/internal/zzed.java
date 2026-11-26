package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public class zzed implements IInterface {
    private final IBinder zzrD;
    private final String zzrE;

    protected zzed(IBinder iBinder, String str) {
        this.zzrD = iBinder;
        this.zzrE = str;
    }

    @Override // android.os.IInterface
    public IBinder asBinder() {
        return this.zzrD;
    }

    protected final Parcel zzZ() {
        Parcel parcelObtain = Parcel.obtain();
        parcelObtain.writeInterfaceToken(this.zzrE);
        return parcelObtain;
    }

    protected final Parcel zza(int i, Parcel parcel) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        try {
            try {
                this.zzrD.transact(i, parcel, parcelObtain, 0);
                parcelObtain.readException();
                parcel.recycle();
                return parcelObtain;
            } catch (RuntimeException e) {
                parcelObtain.recycle();
                throw e;
            }
        } catch (Throwable th) {
            parcel.recycle();
            throw th;
        }
    }

    protected final void zzb(int i, Parcel parcel) throws RemoteException {
        Parcel parcelObtain = Parcel.obtain();
        try {
            this.zzrD.transact(i, parcel, parcelObtain, 0);
            parcelObtain.readException();
        } finally {
            parcel.recycle();
            parcelObtain.recycle();
        }
    }

    protected final void zzc(int i, Parcel parcel) throws RemoteException {
        try {
            this.zzrD.transact(i, parcel, null, 1);
        } finally {
            parcel.recycle();
        }
    }
}
